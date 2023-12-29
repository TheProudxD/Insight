using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    [Inject] private Wallet _wallet;

    public void Initialize(int amount)
    {
        UpdateValue(amount);

        _wallet.CoinsChanged += UpdateValue;
    }

    private void OnDestroy()
    {
        if (_wallet != null)
            _wallet.CoinsChanged -= UpdateValue;
    }

    private void UpdateValue(int value) => _value.text = value.ToString();
}
