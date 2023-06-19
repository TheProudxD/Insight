using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private GameObject _dialogBox;
    private TextMeshProUGUI _dialogText;
    [SerializeField] private string _dialog;
    private bool _playerInRange;

    private void Awake()
    {
        _dialogBox = AssetManager.GetDialogBoxPrefab();
        _dialogText = _dialogBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_playerInRange && _dialogBox!=null)
        {
            _dialogBox.SetActive(true);
            _dialogText.text = _dialog;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = true;
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
