using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TreasureChest : Interactable
{
    private const string OPEN_STATE = "opened";
    
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Item _content;
    [SerializeField] private BoolValue _opened;
    [FormerlySerializedAs("_raisedItem"), SerializeField] private Signal _raiseItem;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_opened.RuntimeValue) OpenAnimation();
    }

    private void Update()
    {
        if (!PlayerInRange) return;

        if (!_opened.RuntimeValue)
        {
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        OpenAnimation();
        _opened.RuntimeValue = true;
        yield return new WaitForSeconds(1);
        
        Context.Raise();
        DialogUI.SetText(_content.ItemDescription);
        DialogBox.SetActive(true);

        _playerInventory.AddItem(_content);
        _playerInventory.CurrentItem = _content;

        _raiseItem.Raise();
        yield return new WaitUntil(() => Input.anyKey);
        
        ChestOpened();
    }

    private void OpenAnimation()
    {
        _animator.SetBool(OPEN_STATE, true);
    }
    private void ChestOpened()
    {
        DialogBox.SetActive(false);
        _raiseItem.Raise();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened.RuntimeValue)
        {
            PlayerInRange = true;
            Context.Raise();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_opened.RuntimeValue)
        {
            PlayerInRange = false;
        }
    }
}