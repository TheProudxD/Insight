using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Inventory Inventory;
        public SpriteRenderer ReceivedItemSprite;
        private PlayerAnimation _playerAnimation;

        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        public void RaiseItem()
        {
            if (Inventory.CurrentItem == null) return;

            if (PlayerCurrentState.Current != PlayerState.Interact)
            {
                _playerAnimation.SetReceiveItemAnimation(true);
                PlayerCurrentState.Current = PlayerState.Interact;
                ReceivedItemSprite.sprite = Inventory.CurrentItem.ItemSprite;
            }
            else
            {
                _playerAnimation.SetReceiveItemAnimation(false);
                PlayerCurrentState.Current = PlayerState.Idle;
                ReceivedItemSprite.sprite = null;
                Inventory.CurrentItem = null;
            }
        }
    }
}