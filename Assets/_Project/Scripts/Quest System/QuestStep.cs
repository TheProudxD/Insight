using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool _isFinished;

        protected void FinishQuestStep()
        {
            if (_isFinished)
                return;

            _isFinished = true;
            Destroy(gameObject);
        }
    }
}