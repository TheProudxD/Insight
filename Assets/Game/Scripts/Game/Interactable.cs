using TMPro;
using UnityEngine;


public abstract class Interactable: MonoBehaviour
{
    //[SerializeField] protected Signal Context;
    protected bool _playerInRange;
    protected GameObject _dialogBox;
    protected TextMeshProUGUI _dialogUI;

    private void Awake()
    {
        _dialogBox = AssetManager.GetDialogBoxPrefab();
        _dialogUI = _dialogBox.GetComponentInChildren<TextMeshProUGUI>();
    }
}