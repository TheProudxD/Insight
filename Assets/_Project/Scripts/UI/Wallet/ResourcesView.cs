using System;
using Extensions;
using Managers;
using ResourceService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourcesView : MonoBehaviour
{
    [Inject] private ResourceManager _resourceManager;
    [Inject] private WindowManager _windowManager;

    [SerializeField] private TMP_Text _softText;
    [SerializeField] private TMP_Text _hardText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button[] _currencyShopOpenerButtons;

    public void OnEnable()
    {
        _resourceManager.ResourceChanged += UpdateValue;
        _currencyShopOpenerButtons.ForEach(b => b.Add(OpenCurrencyShop));

        SetDefaultValue(ResourceType.SoftCurrency);
        SetDefaultValue(ResourceType.HardCurrency);
        SetDefaultValue(ResourceType.Energy);
    }

    private void OpenCurrencyShop() => _windowManager.ShowCurrencyShopWindow();

    private void SetDefaultValue(ResourceType resourceType) =>
        UpdateValue(resourceType, 0, _resourceManager.GetResourceValue(resourceType));

    private void OnDisable()
    {
        if (_resourceManager == null)
            return;

        _currencyShopOpenerButtons.ForEach(b => b.Remove(OpenCurrencyShop));
        _resourceManager.ResourceChanged -= UpdateValue;
    }

    private void UpdateValue(ResourceType resourceType, int oldValue, int newValue)
    {
        switch (resourceType)
        {
            case ResourceType.SoftCurrency:
                _softText.text = newValue.ToString();
                break;
            case ResourceType.HardCurrency:
                _hardText.text = newValue.ToString();
                break;
            case ResourceType.Energy:
                _energyText.text = $"{newValue} / {_resourceManager.MaxEnergyAmount}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
    }
}