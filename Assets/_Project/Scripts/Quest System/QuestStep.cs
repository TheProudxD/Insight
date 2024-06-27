using Unity.VisualScripting;
using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep : MonoBehaviour
    {
        private QuestManager _questManager;
        private bool _isFinished;
        private string _questId;
        private int _stepIndex;

        public void Initialize(QuestManager questManager, string questId, int stepIndex, string questStepState)
        {
            _questManager = questManager;
            _questId = questId;
            _stepIndex = stepIndex;

            if (string.IsNullOrEmpty(questStepState) == false)
            {
                SetQuestStepState(questStepState);
            }
        }

        protected void FinishQuestStep()
        {
            if (_isFinished)
                return;

            _questManager.AdvanceQuest(_questId);
            _isFinished = true;
            Destroy(gameObject);
        }

        protected void ChangeState(string newState)
        {
            _questManager.QuestStepStateChange(_questId, _stepIndex, new QuestStepState(newState));
        }

        protected abstract void SetQuestStepState(string state);

        protected abstract void UpdateState();
    }
}