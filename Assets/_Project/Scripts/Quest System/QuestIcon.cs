using System;
using UnityEngine;

namespace QuestSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class QuestIcon : MonoBehaviour
    {
        [SerializeField] private Sprite _exclamationIcon;
        [SerializeField] private Sprite _questionIcon;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetState(QuestState state, bool startPoint, bool finishPoint)
        {
            switch (state)
            {
                case QuestState.RequirementsNotMet when startPoint:
                    _spriteRenderer.sprite = _exclamationIcon;
                    _spriteRenderer.color = Color.gray;
                    break;
                case QuestState.CanStart when startPoint:
                    _spriteRenderer.sprite = _exclamationIcon;
                    _spriteRenderer.color = Color.yellow;
                    break;
                case QuestState.InProgress when finishPoint:
                    _spriteRenderer.sprite = _questionIcon;
                    _spriteRenderer.color = Color.gray;
                    break;
                case QuestState.CanFinish when finishPoint:
                    _spriteRenderer.sprite = _questionIcon;
                    _spriteRenderer.color = Color.yellow;
                    break;
                case QuestState.Finished:
                    _spriteRenderer.sprite = null;
                    _spriteRenderer.color = Color.white;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}