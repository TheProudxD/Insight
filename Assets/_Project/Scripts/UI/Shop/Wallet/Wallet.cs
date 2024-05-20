using System;
using ResourceService;

public class Wallet
{
    public event Action<ResourceType, int> CurrencyChanged;
    private readonly ResourceManager _resourceManager;

    public Wallet(ResourceManager resourceManager) => _resourceManager = resourceManager;

    public void Add(ResourceType resourceType, int value)
    {
        _resourceManager.AddResource(resourceType, value);

        CurrencyChanged?.Invoke(resourceType, GetCurrentCoins(resourceType));
    }

    public void Spend(ResourceType resourceType, int value)
    {
        _resourceManager.SpendResource(resourceType, value);

        CurrencyChanged?.Invoke(resourceType, GetCurrentCoins(resourceType));
    }

    public bool IsEnough(ResourceType resourceType, int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        return GetCurrentCoins(resourceType) >= value;
    }

    public int GetCurrentCoins(ResourceType resourceType) => _resourceManager.GetResourceValue(resourceType);
}