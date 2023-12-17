using Assets._Project.Scripts.Storage.Static;
using Cysharp.Threading.Tasks;
using SimpleJSON;
using StorageService;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class DataLoader : ILoadingOperation
{
    private readonly string _url;
    private readonly DataManager _dataManager;

    public DataLoader(string url, DataManager dataManager)
    {
        _url = url;
        _dataManager = dataManager;
    }

    public string Description => "Loading data...";

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
        onProcess?.Invoke(0.5f);

        await _dataManager.DownloadMaxLevel();
        onProcess?.Invoke(0.75f);

        await _dataManager.GetDynamicData();
        onProcess?.Invoke(1f);
    }

    private SystemPlayerData ParseSystemPlayerData(string json)
    {
        var data = JSONNode.Parse(json);
        var uid = int.Parse(data["uid"]);
        var key = data["key"].ToString();
        var systemData = new SystemPlayerData(uid, key);
        return systemData;
    }

    private async Task GetSystemData()
    {
        var localPath = Path.Combine(Application.persistentDataPath, DataManager.REGISTRY_DATA_KEY);

        using var wc = new WebClient();
        if (!File.Exists(localPath))
        {
            var remotePath = _url + $"/api.php?action={DataManager.REGISTRY_DATA_KEY}";
            var remoteJson = await wc.DownloadStringTaskAsync(remotePath);
            var remoteData = ParseSystemPlayerData(remoteJson);
            remoteData.ToSingleton();
            await File.WriteAllTextAsync(localPath, JsonUtility.ToJson(remoteData));
        }
        
        else
        {
            var localJsonFile = await File.ReadAllTextAsync(localPath);
            var localData = ParseSystemPlayerData(localJsonFile);
            
            var id = localData.uid;
            var webPath = _url + $"/api.php?playerid={id}&action=systemdata";
            var webJson = await wc.DownloadStringTaskAsync(webPath);
            var webData = ParseSystemPlayerData(webJson);
            if (localData.GetHashCode() != webData.GetHashCode())
            {
                Debug.LogError("Несовпадение данных!");
                //await File.WriteAllTextAsync(localPath, JsonUtility.ToJson(webData));
            }
            
            localData.ToSingleton();
        }
        /*
        {

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
            }
        }
        */
        Debug.Log(SystemPlayerData.Instance.ToString());
        await Task.CompletedTask;
    }

    private bool HasInternet() => Application.internetReachability != NetworkReachability.NotReachable;

    private async Task<bool> TryToConnect(int tryAmount)
    {
        if (HasInternet())
        {
            Debug.Log("<b>Connected!</b>");
            Debug.Log("<color=red>Does not Connected!</color>");
            return true;
        }

        if (tryAmount <= 0)
        {
            Debug.LogError("<color=red>Does not Connected!</color>");
            return false;
        }

        Debug.Log("Waiting...");
        await Task.Delay(2000);
        Debug.Log("Trying to reconnect");
        await TryToConnect(tryAmount - 1);
        return false;
    }
}