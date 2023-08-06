using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class TreasureChest : Interactable
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Item _content;
    [FormerlySerializedAs("_raisedItem"), SerializeField] private Signal _raiseItem;

    private bool _opened;
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();

    private void Update()
    {
        if (!_playerInRange) return;

        if (!_opened)
        {
            OpenChest();
        }
    }

    private async void OpenChest()
    {
        _animator.SetBool("opened", true);
        _opened = true;
        await Task.Delay(1000);
        _dialogUI.SetText(_content.ItemDescription);
        _dialogBox.SetActive(true);
        _playerInventory.AddItem(_content);
        _playerInventory.CurrentItem = _content;
        _raiseItem.Raise();
        await Task.Delay(2000);
        ChestOpened();
    }

    private void ChestOpened()
    {
        _dialogBox.SetActive(false);
        _raiseItem.Raise();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened)
        {
            _playerInRange = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened)
        {
            _playerInRange = false;
        }
    }
}