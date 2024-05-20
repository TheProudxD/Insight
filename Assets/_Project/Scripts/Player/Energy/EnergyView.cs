using TMPro;
using UnityEngine;
using Zenject;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [Inject] private EnergyCounter _energyCounter;

    private void Awake()
    {
        _energyCounter.EnergyChanged += UpdateValue;
        UpdateValue(_energyCounter.Amount, _energyCounter.MaxAmount);
    }

    private void UpdateValue(int value, int maxValue) => _energyText.text = $"{value} / {maxValue}";
}