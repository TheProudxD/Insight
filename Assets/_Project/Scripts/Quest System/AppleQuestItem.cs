using Player;
using UnityEngine;

namespace QuestSystem
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AppleQuestItem : QuestItem
    {
        [SerializeField] private int _appleGained = 1;

        private CollectApplesQuestStep _collectApplesQuest;
        private CircleCollider2D _circleCollider;

        private void Awake()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            //_visual = GetComponentInChildren<SpriteRenderer>();
        }

        protected override void Collect()
        {
            _collectApplesQuest.OnAppleCollected(_appleGained);
            _circleCollider.enabled = false;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.TryGetComponent<PlayerInteraction>(out var player) && _collectApplesQuest != null)
            {
                Collect();
            }
            else
            {
                _collectApplesQuest = FindObjectOfType<CollectApplesQuestStep>();
            }
        }
    }
}