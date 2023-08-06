using TMPro;
using UnityEngine;


public abstract class Interactable: MonoBehaviour
{
    [SerializeField] protected Signal Context;
    protected bool _playerInRange;
    protected GameObject _dialogBox;
    protected TextMeshProUGUI _dialogUI;

    private void Awake()
    {
        _dialogBox = AssetManager.GetDialogBoxPrefab();
        _dialogUI = _dialogBox.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Context.Raise();
            _playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Context.Raise();
            _playerInRange = false;
        }
    }
}