using Cysharp.Threading.Tasks;
using StorageService;
using System;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly IStaticStorageService _staticStorageService;
    private readonly IDynamicStorageService _dynamicStorageService;
    private readonly DataManager _dataManager;

    public DataLoader(DataManager dataManager, IStaticStorageService staticStorageService,
        IDynamicStorageService dynamicStorageService)
    {
        _dataManager = dataManager;
        _staticStorageService = staticStorageService;
        _dynamicStorageService = dynamicStorageService;
    }

    public string Description => "Loading data...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);

        await _staticStorageService.Load(DataManager.STATIC_DATA_KEY, data =>
        {
            if (data is not null)
            {
                //Debug.Log("Static data loaded successfully!");
                Debug.Log(data.ToString());
                _dataManager.SetData(data);
            }
            else
            {
                throw new Exception("File is not found");
            }
        });
        
        onProcess?.Invoke(0.5f);
        
        await _dynamicStorageService.Load(DataManager.DYNAMIC_DATA_KEY, data =>
        {
            if (data is not null)
            {
                //Debug.Log("Dynamic data loaded successfully!");
                Debug.Log(data.ToString());
                _dataManager.SetData(data);
            }
            else
            {
                throw new Exception("File is not found");
            }
        });

        onProcess?.Invoke(1f);
    }
}