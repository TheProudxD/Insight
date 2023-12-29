using System;
using ResourceService;
using StorageService;
using Zenject;

public class Wallet
{
    private readonly DataManager _dataManager;
    public event Action<int> CoinsChanged;
    
    [Inject]
    public Wallet(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _dataManager.ResourceManager.AddResource(ResourceType.SoftCurrency,coins);

        CoinsChanged?.Invoke(GetCurrentCoins());
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _dataManager.ResourceManager.SpendResource(ResourceType.SoftCurrency, coins);
        CoinsChanged?.Invoke(GetCurrentCoins());
    }

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return GetCurrentCoins() >= coins;
    }

    public int GetCurrentCoins() => _dataManager.ResourceManager.GetResourceValue(ResourceType.SoftCurrency);
}