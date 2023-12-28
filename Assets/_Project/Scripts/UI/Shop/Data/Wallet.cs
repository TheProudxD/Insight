using System;
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
    
    public Wallet()
    {
    }

    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _dataManager.AddHardCurrency(coins);

        CoinsChanged?.Invoke(_dataManager.GetHardCurrencyAmount());
    }

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return _dataManager.GetHardCurrencyAmount() >= coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _dataManager.AddHardCurrency(-coins);
        CoinsChanged?.Invoke(_dataManager.GetHardCurrencyAmount());
    }

    public int GetCurrentCoins() => _dataManager.GetHardCurrencyAmount();
}