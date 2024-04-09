using Tools;
using UnityEngine;

namespace Objects
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] private Door _door;
        [SerializeField] private Sprite _activeSprite;
        private bool _pressed;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_pressed)
                ActivateSwitch();
        }

        private void ActivateSwitch()
        {
            _door.Open();
            _pressed = true;
            _spriteRenderer.sprite = _activeSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            ActivateSwitch();
        }
    }
}