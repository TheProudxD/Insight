using UnityEngine;

namespace QuestSystem
{
    public class CollectApplesQuestStep : QuestStep
    {
        [SerializeField] private GameObject _applePrefab;
        [SerializeField] private ObjectPosition _applesToComplete;

        private int _applesCollected;

        private void Awake()
        {
            foreach (var applePosition in _applesToComplete.Positions)
            {
                Instantiate(_applePrefab, applePosition, Quaternion.identity);
            }
        }

        public void OnAppleCollected(int amount)
        {
            _applesCollected += amount;
            UpdateState();
            if (_applesCollected >= _applesToComplete.Count)
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