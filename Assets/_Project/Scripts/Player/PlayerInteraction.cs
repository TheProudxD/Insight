using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Inject] private Inventory _inventory;
        [SerializeField] private SpriteRenderer ReceivedItemSprite;
        
        private PlayerAnimation _playerAnimation;

        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        public void RaiseItem()
        {
            if (_inventory.CurrentItem == null) return;

            if (PlayerCurrentState.Current != PlayerState.Interact)
            {
                _playerAnimation.SetReceiveItemAnimation(true);
                PlayerCurrentState.Current = PlayerState.Interact;
                ReceivedItemSprite.sprite = _inventory.CurrentItem.ItemSprite;
            }
            else
            {
                _playerAnimation.SetReceiveItemAnimation(false);
                PlayerCurrentState.Current = PlayerState.Idle;
                ReceivedItemSprite.sprite = null;
                _inventory.CurrentItem = null;
            }
        }
    }
}