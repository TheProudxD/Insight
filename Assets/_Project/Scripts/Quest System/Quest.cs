using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QuestSystem
{
    public class Quest
    {
        private readonly QuestManager _questManager;
        private readonly QuestStepState[] _questStepStates;
        private int _currentQuestStepIndex;

        public QuestInfo Info { get; }
        public QuestState State { get; set; }

        public Quest(QuestManager questManager, QuestInfo info)
        {
            _questManager = questManager;
            Info = info;

            State = QuestState.RequirementsNotMet;
            _currentQuestStepIndex = 0;
            _questStepStates = new QuestStepState[Info.QuestStepPrefabs.Length];
            for (var index = 0; index < _questStepStates.Length; index++)
            {
                _questStepStates[index] = new QuestStepState();
            }
        }

        public Quest(QuestManager questManager, QuestStepState[] questStepStates, int currentQuestStepIndex,
            QuestInfo info, QuestState state)
        {
            _questManager = questManager;
            _questStepStates = questStepStates;
            _currentQuestStepIndex = currentQuestStepIndex;
            Info = info;
            State = state;
            
            if (_questStepStates.Length != Info.QuestStepPrefabs.Length)
            {
                Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                                 + "of different lengths. This indicates something changed "
                                 + "with the QuestInfo and the saved data is now out of sync. "
                                 + "Reset your data - as this might cause issues. QuestId: " + Info.ID);
            }
        }

        private QuestStep GetCurrentQuestStepPrefab()
        {
            if (CurrentStepExists())
                return Info.QuestStepPrefabs[_currentQuestStepIndex];

            Debug.LogWarning("There is no current step");
            return null;
        }

        public bool CurrentStepExists() =>
            _currentQuestStepIndex < Info.QuestStepPrefabs.Length;

        public void MoveToNextStep() => _currentQuestStepIndex++;

        public void InstantiateCurrentStepPrefab(Transform transform)
        {
            var questStep = GetCurrentQuestStepPrefab();
            if (questStep != null)
            {
                var step = Object.Instantiate(questStep, transform);
                step.Initialize(_questManager, Info.ID, _currentQuestStepIndex,
                    _questStepStates[_currentQuestStepIndex].State);
            }
        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < _questStepStates.Length)
            {
                _questStepStates[stepIndex].State = questStepState.State;
            }
            else
            {
                Debug.LogWarning($"Step index if out of range: {stepIndex}, ID: {Info.ID}");
            }
        }

        public QuestData GetData() => new(State, _currentQuestStepIndex, _questStepStates);
        
        public string GetFullStatusText()
        {
            string fullStatus = "";

            if (State == QuestState.RequirementsNotMet)
            {
                fullStatus = "Requirements are not yet met to start this quest.";
            }
            else if (State == QuestState.CanStart)
            {
                fullStatus = "This quest can be started!";
            }
            else 
            {
                // display all previous quests with strikethroughs
                for (int i = 0; i < _currentQuestStepIndex; i++)
                {
                    fullStatus += "<s>" + _questStepStates[i].State + "</s>\n";
                }
                // display the current step, if it exists
                if (CurrentStepExists())
                {
                    fullStatus += _questStepStates[_currentQuestStepIndex].State;
                }

                switch (State)
                {
                    // when the quest is completed or turned in
                    case QuestState.CanFinish:
                        fullStatus += "The quest is ready to be turned in.";
                        break;
                    case QuestState.Finished:
                        fullStatus += "The quest has been completed!";
                        break;
                    case QuestState.RequirementsNotMet:
                        break;
                    case QuestState.CanStart:
                        break;
                    case QuestState.InProgress:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return fullStatus;
        }
    }
}