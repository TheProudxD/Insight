using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TreasureChest : Interactable
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Item _content;

    [FormerlySerializedAs("_raisedItem"), SerializeField]
    private Signal _raiseItem;

    private bool _opened;
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();

    private void Update()
    {
        if (!PlayerInRange) return;

        if (!_opened)
        {
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        _animator.SetBool("opened", true);
        _opened = true;
        yield return new WaitForSeconds(1);
        DialogUI.SetText(_content.ItemDescription);
        DialogBox.SetActive(true);
        
        _playerInventory.AddItem(_content);
        _playerInventory.CurrentItem = _content;
        
        _raiseItem.Raise();
        yield return new WaitUntil(() => Input.anyKey);
        ChestOpened();
    }

    private void ChestOpened()
    {
        DialogBox.SetActive(false);
        _raiseItem.Raise();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened)
        {
            PlayerInRange = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened)
        {
            PlayerInRange = false;
        }
    }
}