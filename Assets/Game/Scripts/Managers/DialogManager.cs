using TMPro;
using UnityEngine;

public class DialogManager : Interactable
{
    [SerializeField] private string _dialog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _dialogUI.text = _dialog;
            _dialogBox.SetActive(true);
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !other.isTrigger)
        {
            _playerInRange = false;
            _dialogBox.SetActive(false);
        }
    }
}