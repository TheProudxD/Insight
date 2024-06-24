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
            UpdateState();
            if (_applesCollected >= _applesToComplete)
            {
                FinishQuestStep();
            }
        }

        protected override void SetQuestStepState(string state)
        {
            if (int.TryParse(state, out var result))
            {
                _applesCollected = result;
                UpdateState();
            }
            else
            {
                Debug.LogError($"error while parse quest step state: {state}");
            }
        }

        protected override void UpdateState()
        {
            var state = _applesCollected.ToString();
            ChangeState(state);
        }
    }
}