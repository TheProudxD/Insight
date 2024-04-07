using Assets._Project.Scripts.Storage.Static;
using Cysharp.Threading.Tasks;
using SimpleJSON;
using StorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly IDynamicStorageService _dynamicStorageService;
    private readonly DataManager _dataManager;
    private readonly ConnectionManager _connectionManager;

    public DataLoader(IDynamicStorageService dynamicStorageService, DataManager dataManager, ConnectionManager connectionManager)
    {
        _dynamicStorageService = dynamicStorageService;
        _dataManager = dataManager;
        _connectionManager = connectionManager;
    }

    public string Description => "Loading data...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0f);

        var isConnected = await _connectionManager.Connect();
        if (!isConnected)
        {
            Application.Quit();
            return;
        }

        onProcess?.Invoke(0.35f);

        var result = await GetSystemData();
        if (!result)
        {
            Debug.LogError("Error. Mismatch data!");
            Time.timeScale = 0;
            return;
        }

        onProcess?.Invoke(0.5f);

        InsightUtils.IsCorrectShopItemsId();
        onProcess?.Invoke(0.6f);

        await _dataManager.DownloadMaxLevel();
        onProcess?.Invoke(0.75f);

        await _dataManager.GetDynamicData();
        onProcess?.Invoke(1f);
    }

    private SystemPlayerData ParseSystemPlayerData(JSONNode data)
    {
        var uid = int.Parse(data["uid"]);
        var key = data["key"].Value;
        var systemData = new SystemPlayerData(uid, key);
        return systemData;
    }

    private async UniTask<bool> GetSystemData()
    {
        var localPath = Path.Combine(Application.persistentDataPath, DataManager.REGISTRY_DATA_KEY);

        using var wc = new WebClient();

        if (!File.Exists(localPath))
        {
            var downloadParams = new Dictionary<string, string>
            {
                { "action", DataManager.REGISTRY_DATA_KEY },
            };
            var remoteJson = await _dynamicStorageService.Download(downloadParams);
            var remoteData = ParseSystemPlayerData(remoteJson);
            remoteData.ToSingleton();
            await File.WriteAllTextAsync(localPath, JsonUtility.ToJson(remoteData));
        }
        else
        {
            var localJsonFile = await File.ReadAllTextAsync(localPath);
            var jsonNode = JSONNode.Parse(localJsonFile);
            var localData = ParseSystemPlayerData(jsonNode);

            var downloadParams = new Dictionary<string, string>
            {
                { "playerid", localData.uid.ToString() },
                { "action", "systemdata" },
            };
            var webJson = await _dynamicStorageService.Download(downloadParams);
            var webData = ParseSystemPlayerData(webJson);

            if (localData.GetHashCode() != webData.GetHashCode())
            {
                return false;
            }

            localData.ToSingleton();
        }

        Debug.Log(SystemPlayerData.Instance.ToString());
        await Task.CompletedTask;
        return true;
    }
}