using System;
using StorageService;
using Zenject;

public class Wallet
{
    [Inject] private DataManager _dataManager;
    public event Action<int> CoinsChanged;

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