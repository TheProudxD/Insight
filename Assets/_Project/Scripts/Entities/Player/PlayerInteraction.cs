using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Inject] private CharacterAudioPlayer _characterAudioPlayer;
        
        [FormerlySerializedAs("ReceivedItemSprite"), SerializeField]
        private SpriteRenderer _receivedItemSprite;

        private PlayerAnimation _playerAnimation;

        private void Awake() => _playerAnimation = GetComponent<PlayerAnimation>();

        public void DisplayPickupItem(InventoryItem item)
        {
            if (item == null)
                return;

            _playerAnimation.SetReceiveItemAnimation(true);
            PlayerStateMachine.Current = PlayerState.Interact;
            _receivedItemSprite.sprite = item.Image;
            _characterAudioPlayer.PlayPickupItemSound();
        }

        public void RemovePickupItem()
        {
            _playerAnimation.SetReceiveItemAnimation(false);
            PlayerStateMachine.Current = PlayerState.Idle;
            _receivedItemSprite.sprite = null;
        }
    }
}