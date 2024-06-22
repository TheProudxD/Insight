using UnityEngine;

namespace QuestSystem
{
    public class Quest
    {
        private int _currentQuestStepIndex;
        public QuestInfo Info { get; private set; }
        public QuestState State { get; private set; }

        public Quest(QuestInfo info)
        {
            Info = info;
            State = QuestState.RequirementsNotMet;
            _currentQuestStepIndex = 0;
        }

        private void MoveToNextStep()
        {
            _currentQuestStepIndex++;
        }

        private bool CurrentStepExists() =>
            _currentQuestStepIndex < Info.QuestPrerequisites.Length;

        private QuestStep GetCurrentQuestStepPrefab()
        {
            if (CurrentStepExists())
                return Info.QuestStepPrefabs[_currentQuestStepIndex];

            Debug.LogWarning("There is no current step");
            return null;
        }

        private void InstantiateCurrentStepPrefab(Transform transform)
        {
            var questStep = GetCurrentQuestStepPrefab();
            if (questStep != null)
            {
                Object.Instantiate(questStep, transform);
            }
        }
    }
}