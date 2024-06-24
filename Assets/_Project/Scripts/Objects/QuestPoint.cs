using System;
using QuestSystem;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(QuestIcon))]
    [RequireComponent(typeof(Collider2D))]
    public class QuestPoint : Interactable
    {
        [SerializeField] private QuestInfo _questInfo;
        [SerializeField] private bool _startPoint = true;
        [SerializeField] private bool _finishPoint = true;
        
        private QuestState _currentState;
        private QuestManager _questManager;
        private QuestIcon _questIcon;

        protected virtual void Awake()
        {
            _questManager = FindObjectOfType<QuestManager>();
            _questIcon = GetComponent<QuestIcon>();
        }

        private void OnEnable()
        {
            _questManager.QuestStateChanged += ChangeQuestState;
        }

        private void OnDisable()
        {
            _questManager.QuestStateChanged -= ChangeQuestState;
        }

        public void ChangeQuestState(Quest quest)
        {
            if (quest.Info.ID == _questInfo.ID)
            {
                _currentState = quest.State;
                _questIcon.SetState(quest.State, _startPoint, _finishPoint);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            switch (_currentState)
            {
                case QuestState.CanStart when _startPoint:
                    _questManager.StartQuest(_questInfo.ID);
                    break;
                case QuestState.CanFinish when _finishPoint:
                    _questManager.FinishQuest(_questInfo.ID);
                    break;
                case QuestState.RequirementsNotMet:
                    break;
                case QuestState.InProgress:
                    break;
                case QuestState.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}