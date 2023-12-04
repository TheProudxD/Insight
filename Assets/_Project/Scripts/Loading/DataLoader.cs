using Assets._Project.Scripts.Storage.Static;
using Cysharp.Threading.Tasks;
using SimpleJSON;
using StorageService;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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

    private async Task GetSystemData()
    {
        var localPath = Path.Combine(Application.persistentDataPath, DataManager.SYSTEM_DATA_KEY);

        if (!File.Exists(localPath))
        {
            using var wc = new WebClient();
            var path = "http://game.ispu.ru/insight" + $"/api.php?action={DataManager.SYSTEM_DATA_KEY}";
            var value = await wc.DownloadStringTaskAsync(path);
            var systemData = new SystemData();
            var json = JSONNode.Parse(value);
            systemData.UID = int.Parse(json["uid"]);
            File.WriteAllText(localPath, JsonUtility.ToJson(systemData));
        }
        else
        {
            var localJsonFile = File.ReadAllText(localPath);
            JsonUtility.FromJson<SystemData>(localJsonFile);
        }

        await Task.CompletedTask;
    }

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);

        await GetSystemData();

        onProcess?.Invoke(0.25f);

        //Debug.LogWarning(SystemData.Instance?.UID);
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