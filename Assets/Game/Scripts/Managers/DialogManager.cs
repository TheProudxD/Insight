using UnityEngine;

public class DialogManager : Interactable
{
    [SerializeField] private string _dialog;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _dialogUI.text = _dialog;
            _dialogBox.SetActive(true);
            _playerInRange = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _playerInRange = false;
            _dialogBox.SetActive(false);
        }
    }
}