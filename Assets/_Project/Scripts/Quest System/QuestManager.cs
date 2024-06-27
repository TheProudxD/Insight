using System;
using System.Collections.Generic;
using ResourceService;
using Storage;
using UnityEngine;
using Zenject;

namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [Inject] private ResourceManager _resourceManager;
        [Inject] private SceneManager _sceneManager;

        public Action<Quest> QuestStateChanged;

        private readonly bool _loadQuestState = true;
        private Dictionary<string, Quest> _questMap;

        private void Awake()
        {
            _questMap = CreateQuestMap();
        }

        private void OnEnable()
        {
            _sceneManager.LevelChanged += InitializeQuests;
            InitializeQuests();
        }

        private void OnDisable()
        {
            _sceneManager.LevelChanged -= InitializeQuests;
        }

        private void Update()
        {
            foreach (var quest in _questMap.Values)
            {
                if (quest.State == QuestState.RequirementsNotMet && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.Info.ID, QuestState.CanStart);
                    print("change CanStart " + quest.Info.DisplayName);
                }
            }
        }

        private void InitializeQuests(Scene scene) => InitializeQuests();

        private void InitializeQuests()
        {
            foreach (var quest in _questMap.Values)
            {
                if (quest.State == QuestState.InProgress)
                {
                    quest.InstantiateCurrentStepPrefab(transform);
                }

                QuestStateChanged?.Invoke(quest);
            }
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            var allQuests = Resources.LoadAll<QuestInfo>("Quests");
            var idToQuestMap = new Dictionary<string, Quest>();
            foreach (var questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.ID))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.ID);
                }

                idToQuestMap.Add(questInfo.ID, LoadQuest(questInfo));
            }

            return idToQuestMap;
        }

        private Quest GetQuestByID(string id)
        {
            if (_questMap.TryGetValue(id, out var quest))
            {
                return quest;
            }

            Debug.LogError("ID not found in the Quest Map: " + id);
            return null;
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            var quest = GetQuestByID(id);
            quest.State = state;
            QuestStateChanged?.Invoke(quest);
        }

        private bool CheckRequirementsMet(Quest quest)
        {
            var metRequirements = true;

            foreach (var prerequisite in quest.Info.QuestPrerequisites)
            {
                if (GetQuestByID(prerequisite.ID).State != QuestState.Finished)
                {
                    metRequirements = false;
                }
            }

            return metRequirements;
        }

        private void ClaimRewards(Quest quest)
        {
            foreach (var resource in quest.Info.Rewards)
            {
                _resourceManager.Add(resource.Type, resource.Amount);
            }
        }

        public void StartQuest(string id)
        {
            var quest = GetQuestByID(id);
            quest.InstantiateCurrentStepPrefab(transform);
            ChangeQuestState(id, QuestState.InProgress);
        }

        public void AdvanceQuest(string id)
        {
            var quest = GetQuestByID(id);
            quest.MoveToNextStep();
            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentStepPrefab(transform);
            }
            else
            {
                ChangeQuestState(quest.Info.ID, QuestState.CanFinish);
            }
        }

        public void FinishQuest(string id)
        {
            var quest = GetQuestByID(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.Info.ID, QuestState.Finished);
        }

        public void QuestStepStateChange(string questId, int stepIndex, QuestStepState questStepState)
        {
            var quest = GetQuestByID(questId);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(questId, quest.State);
        }

        private Quest LoadQuest(QuestInfo questInfo)
        {
            Quest quest;
            try
            {
                if (PlayerPrefs.HasKey(questInfo.ID) && _loadQuestState)
                {
                    var serializedData = PlayerPrefs.GetString(questInfo.ID);
                    var data = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(this, data.QuestStepStates, data.QuestStepIndex, questInfo, data.State);
                }
                else
                {
                    quest = new Quest(this, questInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"error loading quest {questInfo.ID}: {e}");
                throw;
            }

            return quest;
        }

        private void SaveQuest(Quest quest)
        {
            try
            {
                var data = quest.GetData();
                var serializedData = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(quest.Info.ID, serializedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"error saving quest {quest.Info.ID}: {e}");
            }
        }

        private void OnApplicationQuit()
        {
            foreach (var quest in _questMap.Values)
            {
                SaveQuest(quest);
            }
        }
    }
}