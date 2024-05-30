using System;
using ResourceService;
using TMPro;
using UnityEngine;
using Zenject;

public class ResourcesView : MonoBehaviour
{
    [SerializeField] private TMP_Text _softText;
    [SerializeField] private TMP_Text _hardText;
    [SerializeField] private TMP_Text _energyText;

    [Inject] private ResourceManager _resourceManager;

    public void Awake()
    {
        _resourceManager.ResourceChanged += UpdateValue;

        SetDefaultValue(ResourceType.SoftCurrency);
        SetDefaultValue(ResourceType.HardCurrency);
        SetDefaultValue(ResourceType.Energy);
    }

    private void SetDefaultValue(ResourceType resourceType) =>
        UpdateValue(resourceType, 0, _resourceManager.GetResourceValue(resourceType));

    private void OnDestroy()
    {
        if (_resourceManager == null)
            return;

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