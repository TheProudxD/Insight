using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    [Inject] private Wallet _wallet;

    public void Initialize()
    {
        if (_wallet == null)
            print("wtf bro");
        UpdateValue(_wallet.GetCurrentCoins());

        _wallet.CoinsChanged += UpdateValue;
    }

    private void OnDestroy()
    {
        if (_wallet != null) _wallet.CoinsChanged -= UpdateValue;
    }

    private void UpdateValue(int value) => _value.text = value.ToString();
}
