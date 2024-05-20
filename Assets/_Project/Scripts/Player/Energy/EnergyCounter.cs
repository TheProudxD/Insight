using System;
using System.Collections.Generic;
using Assets._Project.Scripts.Storage.Static;
using StorageService;
using UnityEngine;

public class EnergyCounter
{
    private const string CHANGE_ENERGY_KEY = "changeenergy";

    public event Action<int, int> EnergyChanged;

    private readonly IDynamicStorageService _dynamicStorageService;
    private PlayerData _playerData;

    public int Amount { get; private set; } = 30;
    public int MaxAmount { get; private set; } = 50;

    private EnergyCounter(IDynamicStorageService dynamicStorageService, DataManager dataManager)
    {
        _dynamicStorageService = dynamicStorageService;
        dataManager.DataLoaded += Initialize;
    }

    private void Initialize(PlayerData playerData)
    {
        _playerData = playerData;
        EnergyChanged?.Invoke(Amount, MaxAmount);
    }

    public void Add(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        Amount += value;

        SaveEnergy();

        EnergyChanged?.Invoke(Amount, MaxAmount);
    }

    public void Spend(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        if (Amount - value < 0)
            throw new ArgumentException("You can't spend more than is in current amount");

        Amount -= value;

        SaveEnergy();

        EnergyChanged?.Invoke(Amount, MaxAmount);
    }

    public bool IsEnough(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        return Amount >= value;
    }

    private async void SaveEnergy()
    {
        var uploadParams = new Dictionary<string, string>
        {
            { "playercurrency", Amount.ToString() },
            { "action", CHANGE_ENERGY_KEY },
            { "playerid", SystemPlayerData.Instance.uid.ToString() },
        };

        await _dynamicStorageService.Upload(uploadParams, result =>
        {
            if (result)
            {
                _playerData.AmountEnergy = Amount;
                Debug.Log($"Soft Currency saved Successfully to {Amount}");
            }
            else
            {
                Debug.Log("Error while saving Soft Currency");
            }
        });
    }
}