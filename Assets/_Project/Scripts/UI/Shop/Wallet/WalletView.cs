using System;
using ResourceService;
using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _softText;
    [SerializeField] private TMP_Text _hardText;

    [Inject] private Wallet _wallet;

    public void Initialize(int softAmount, int hardAmount)
    {
        _wallet.CurrencyChanged += UpdateValue;

        UpdateValue(ResourceType.SoftCurrency, softAmount);
        UpdateValue(ResourceType.HardCurrency, hardAmount);
    }

    private void OnDestroy()
    {
        if (_wallet == null)
            return;

        _wallet.CurrencyChanged -= UpdateValue;
    }

    private void UpdateValue(ResourceType resourceType, int value)
    {
        switch (resourceType)
        {
            case ResourceType.SoftCurrency:
                _softText.text = value.ToString();
                break;
            case ResourceType.HardCurrency:
                _hardText.text = value.ToString();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
    }
}