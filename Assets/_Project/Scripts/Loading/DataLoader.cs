using Cysharp.Threading.Tasks;
using StorageService;
using System;
using Tools;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly DataManager _dataManager;

    public DataLoader(DataManager dataManager) => _dataManager = dataManager;

    public string Description => "Loading data...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);
        
        var result = await _dataManager.GetSystemData();
        if (result == false)
        {
            Debug.LogError("Error. Mismatch data!");
            Time.timeScale = 0;
            return;
        }

        onProcess?.Invoke(0.25f);

        InsightUtils.IsCorrectShopItemsId();
        onProcess?.Invoke(0.5f);

        await _dataManager.GetGameData();
        onProcess?.Invoke(0.75f);

        await _dataManager.GetPlayerData();
        onProcess?.Invoke(1f);
    }
}