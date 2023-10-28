using Cysharp.Threading.Tasks;
using StorageService;
using System;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly IStorageService _storageService;
    private readonly ITransferService _transferService;
    private readonly DataManager _dataManager;

    public DataLoader(DataManager dataManager, IStorageService storageService, ITransferService transferService)
    {
        _dataManager = dataManager;
        _storageService = storageService;
        _transferService = transferService;
    }

    public string Description => "Loading files from server...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        
        await _storageService.Load(DataManager.JSON_DATA_KEY, data =>
        {
            if (data is not null)
            {
                //Debug.Log("AllData loaded successfully!");
                Debug.Log(data.ToString());
                _dataManager.GetData(data);
            }
            else
            {
                throw new Exception("File is not found");
            }
        });

        await _transferService.Load(DataManager.DATABASE_DATA_KEY, data =>
        {
            if (data is not null)
            {
                //Debug.Log("AllData loaded successfully!");
                Debug.Log(data.ToString());
                _dataManager.GetData(data);
            }
            else
            {
                throw new Exception("File is not found");
            }
        });
        onProcess?.Invoke(1f);
    }
}