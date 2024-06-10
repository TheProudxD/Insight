using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Managers;
using SimpleJSON;
using Storage.Static;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace StorageService
{
    public class DataManager
    {
        public event Action<PlayerData> DataLoaded;

        public const string SYSTEM_DATA_KEY = "systemdata";
        public const string REGISTRY_DATA_KEY = "registry";
        public const string CHANGE_NAME_KEY = "changename";
        public const string MAX_LEVEL_DATA_KEY = "maxleveldata";
        private const string DYNAMIC_USER_DATA_KEY = "userdata";
        private const string DEFAULT_PLAYER_NAME = "Player";

        private readonly WindowManager _windowManager;
        private readonly GameData _gameData = new();
        private PlayerData _playerData;
        private IStaticStorageService StaticStorageService { get; }
        private IDynamicStorageService DynamicStorageService { get; }

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService, WindowManager windowManager)
        {
            StaticStorageService = staticStorageService;
            DynamicStorageService = dynamicStorageService;
            _windowManager = windowManager;
        }

        public async UniTask SetName(string newName)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playername", newName },
                { "action", CHANGE_NAME_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.Name = newName;
                    Debug.Log($"Renamed successfully to {newName}");
                }
                else
                {
                    Debug.Log("Error while renaming");
                }
            });

            DataLoaded?.Invoke(_playerData);
        }

        public async UniTask DownloadMaxLevel() =>
            await StaticStorageService.Download(MAX_LEVEL_DATA_KEY, data =>
            {
                if (data is null)
                    throw new Exception("File is not found");

                Debug.Log("MaxLevel: " + data.MaxLevel);
                _gameData.MaxLevel = data.MaxLevel;
            });

        public async UniTask<int> GetMaxLevel()
        {
            if (_gameData is { MaxLevel: > 0 })
                return _gameData.MaxLevel;

            await DownloadMaxLevel();

            return _gameData.MaxLevel;
        }
        
        public async UniTask<bool> GetPlayerData()
        {
            if (_playerData != null)
                return true;

            var downloadParams = new Dictionary<string, string>
            {
                { "action", DYNAMIC_USER_DATA_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            var callbackData = await DynamicStorageService.Download(downloadParams);
            _playerData = new PlayerData
            {
                AmountHardResources = callbackData["HardCurrency"],
                AmountSoftResources = callbackData["SoftCurrency"],
                MaxPassedLevel = callbackData["lvl"],
                Name = callbackData["Name"],
                AmountEnergy = callbackData["Energy"],
            };

            DataLoaded?.Invoke(_playerData);

            if (_playerData.Name == DEFAULT_PLAYER_NAME)
            {
                await SetName("Player " + SystemPlayerData.Instance.uid);
            }

            if (_playerData.MaxPassedLevel > _gameData.MaxLevel)
            {
                Debug.LogError("Max level less than current!");
                return false;
            }

            Debug.Log(_playerData.ToString());
            await Task.CompletedTask;
            return true;
        }
        
        public async UniTask<bool> GetSystemData()
        {
            var localPath = Path.Combine(Application.persistentDataPath, REGISTRY_DATA_KEY);

            using var wc = new WebClient();

            if (File.Exists(localPath) == false || File.ReadLines(localPath).Any() == false)
            {
                _windowManager.ShowSignupWindow();

                var downloadParams = new Dictionary<string, string>
                {
                    { "action", REGISTRY_DATA_KEY },
                };

                var remoteJson = await DynamicStorageService.Download(downloadParams);
                var remoteData = SystemPlayerData.Parse(remoteJson);
                remoteData.ToSingleton();

                await using var f = File.CreateText(localPath);
                await f.WriteAsync(remoteJson.ToString());
                f.Close();
            }
            else
            {
                //_windowManager.ShowLoginWindow();
                
                var localJsonFile = await File.ReadAllTextAsync(localPath);
                var jsonNode = JSONNode.Parse(localJsonFile);
                var localData = SystemPlayerData.Parse(jsonNode);

                var downloadParams = new Dictionary<string, string>
                {
                    { "playerid", localData.uid.ToString() },
                    { "action", SYSTEM_DATA_KEY },
                };
                var webJson = await DynamicStorageService.Download(downloadParams);
                var webData = SystemPlayerData.Parse(webJson);

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
}