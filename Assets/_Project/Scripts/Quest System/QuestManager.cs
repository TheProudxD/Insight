using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<string, Quest> _questMap;

        private void Awake()
        {
            _questMap = CreateQuestMap();

            Quest quest = GetQuestByID("CollectApplesQuests");
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

                idToQuestMap.Add(questInfo.ID, new Quest(questInfo));
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

        public void StartQuest(string id)
        {
        }

        public void AdvanceQuest(string id)
        {
        }

        public void FinishQuest(string id)
        {
        }
    }
}