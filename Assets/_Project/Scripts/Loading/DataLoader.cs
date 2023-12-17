using Assets._Project.Scripts.Storage.Static;
using Cysharp.Threading.Tasks;
using SimpleJSON;
using StorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly string _url;
    private readonly IStaticStorageService _staticStorageService;
    private readonly IDynamicStorageService _dynamicStorageService;
    private readonly DataManager _dataManager;
    private readonly string _defaultPlayerName = "Player";

    public DataLoader(string url, DataManager dataManager, IStaticStorageService staticStorageService,
        IDynamicStorageService dynamicStorageService)
    {
        _url = url;
        _dataManager = dataManager;
        _staticStorageService = staticStorageService;
        _dynamicStorageService = dynamicStorageService;
    }

    public string Description => "Loading data...";

    private SystemPlayerData ParserSysPlayerData(string json)
    {
        var data = JSONNode.Parse(json);
        var uid = int.Parse(data["uid"]);
        var key = data["key"];
        var systemData = new SystemPlayerData(uid, key);
        return systemData;
    }

    private async Task GetSystemData()
    {
        var localPath = Path.Combine(Application.persistentDataPath, DataManager.REGISTRY_DATA_KEY);

        if (!File.Exists(localPath))
        {
            using var wc = new WebClient();
            var path = _url + $"/api.php?action={DataManager.REGISTRY_DATA_KEY}";
            var json = await wc.DownloadStringTaskAsync(path);
            var systemData = ParserSysPlayerData(json);

            await File.WriteAllTextAsync(localPath, JsonUtility.ToJson(systemData));
        }
        else
        {
            var localJsonFile = await File.ReadAllTextAsync(localPath);
            var localData = ParserSysPlayerData(localJsonFile);

            /*
            using var wc = new WebClient();
            var path = _url + $"/api.php?action={DataManager.PLAYER_DATA_KEY}";
            var value = await wc.DownloadStringTaskAsync(path);
            var webJson = JSONNode.Parse(value);
            var WUID = int.Parse(webJson["uid"]);
            var WName = "Player " + UID; //json["name"];
            var WKey = 17; //json["key"];
            var webdata = new SystemPlayerData(WUID, WName, WKey);

            if (localData.GetHashCode() != webdata.GetHashCode())
            {
                Debug.LogError("Данные изменились");
                await File.WriteAllTextAsync(localPath, value);
            }*/
        }

        await Task.CompletedTask;
    }

    private bool HasInternet() => Application.internetReachability != NetworkReachability.NotReachable;

    private async Task<bool> TryToConnect(int tryAmount)
    {
        if (HasInternet())
        {
            Debug.Log("Connected!");
            return true;
        }

        if (tryAmount == 0)
        {
            Debug.Log("Does not Connected!");
            return false;
        }

        Debug.Log("Waiting...");
        await Task.Delay(2000);
        Debug.Log("Trying to reconnect");
        await TryToConnect(tryAmount - 1);
        return false;
    }

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);

        var isConnected = await TryToConnect(3);
        if (!isConnected)
        {
            Debug.LogError("Error. Check internet connection!");
            Application.Quit();
            return;
        }

        onProcess?.Invoke(0.25f);

        await GetSystemData();
        Debug.Log(SystemPlayerData.Instance.ToString());

        onProcess?.Invoke(0.5f);
        await GetMaxLevel();

        onProcess?.Invoke(0.75f);

        await GetDynamicData();

        onProcess?.Invoke(1f);
    }

    private async Task GetMaxLevel()
    {
        await _staticStorageService.Download(DataManager.MAX_LEVEL_DATA_KEY, data =>
        {
            if (data is not null)
            {
                Debug.Log("MaxLevel: " + data.MaxLevel);
                _dataManager.SetData(data);
            }
            else
            {
                throw new Exception("File is not found");
            }
        });
    }

    private async Task GetDynamicData()
    {
        var downloadParams = new Dictionary<string, string>
        {
            { "action", DataManager.DYNAMIC_USER_DATA_KEY },
            { "playerid", SystemPlayerData.Instance.uid.ToString() },
        };

        await _dynamicStorageService.Download(downloadParams, Callback);
        return;

        async void Callback(DynamicPlayerData data)
        {
            if (data is not null)
            {
                _dataManager.SetData(data);
                if (data.Name == _defaultPlayerName)
                    await _dataManager.SetName("Player " + SystemPlayerData.Instance.uid);
                Debug.Log(data.ToString());
            }
            else
            {
                throw new Exception("dynamic data is null");
            }
        }
    }
}