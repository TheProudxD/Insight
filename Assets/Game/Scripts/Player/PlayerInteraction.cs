using UnityEngine;

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

        if (PlayerController.CurrentState != PlayerState.Interact)
        {
            _playerAnimation.SetReceiveItemAnimation(true);
            PlayerController.CurrentState = PlayerState.Interact;
            ReceivedItemSprite.sprite = Inventory.CurrentItem.ItemSprite;
        }
        else
        {
            _playerAnimation.SetReceiveItemAnimation(false);
            PlayerController.CurrentState = PlayerState.Idle;
            ReceivedItemSprite.sprite = null;
            Inventory.CurrentItem = null;
        }
    }
}