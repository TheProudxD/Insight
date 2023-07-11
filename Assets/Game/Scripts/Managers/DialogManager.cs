using TMPro;
using UnityEngine;

public class DialogManager : Interactable
{
    [SerializeField] private string _dialog;
    private GameObject _dialogBox;
    private TextMeshProUGUI _dialogUI;

    private void Awake()
    {
        _dialogBox = AssetManager.GetDialogBoxPrefab();
        _dialogUI = _dialogBox.GetComponentInChildren<TextMeshProUGUI>();
        _dialogUI.text = _dialog;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _dialogBox.SetActive(true);
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            _dialogBox.SetActive(false);
        }
    }
}