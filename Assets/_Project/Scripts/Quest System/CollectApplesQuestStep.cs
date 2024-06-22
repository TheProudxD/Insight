using UnityEngine;

namespace QuestSystem
{
    public class CollectApplesQuestStep : QuestStep
    {
        private int _applesCollected;
        [SerializeField] private int _applesToComplete = 5;

        public void OnAppleCollected(int amount)
        {
            _applesCollected += amount;
            if (_applesCollected >= _applesToComplete)
            {
                FinishQuestStep();
            }
        }
    }
}