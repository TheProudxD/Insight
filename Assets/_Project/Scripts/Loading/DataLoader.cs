using Assets._Project.Scripts.Storage.Static;
using Cysharp.Threading.Tasks;
using StorageService;
using System;
using System.IO;
using System.Net;
using Tools;
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

        /*
        var localPath = Utils.BuildPath(DataManager.SYSTEM_DATA_KEY);
        if (File.Exists(localPath))
        {
            var localJsonFile = File.ReadAllText(localPath);
            var systemData = JsonUtility.FromJson<SystemData>(localJsonFile);
        }
        else
        {
            using var wc = new WebClient();
            var value = await wc.DownloadStringTaskAsync("http://game.ispu.ru/insight" + $"/api.php?action={DataManager.SYSTEM_DATA_KEY}");
        }
        Debug.LogWarning(systemData);

        */
        onProcess?.Invoke(0.25f);

        await _staticStorageService.Download(DataManager.STATIC_DATA_KEY, data =>
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
        
        await _dynamicStorageService.Download(DataManager.DYNAMIC_DATA_KEY, data =>
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