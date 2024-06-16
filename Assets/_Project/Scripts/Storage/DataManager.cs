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
using UnityEngine.UIElements;

namespace StorageService
{
    public class NickNameChanger
    {
    }

    public class DataManager
    {
        public event Action<PlayerData> PlayerDataLoaded;
        public event Action<GameData> GameDataLoaded;

        public const string GAME_DATA_KEY = "gameconfig";
        private const string SYSTEM_DATA_KEY = "systemdata";
        private const string DYNAMIC_USER_DATA_KEY = "userdata";
        private const string REGISTRY_DATA_KEY = "registry";
        private const string CHANGE_NAME_KEY = "changename";
        private const string PLAYER_ENTERED = "energyEntered";
        private const string DEFAULT_PLAYER_NAME = "Player";

        private readonly WindowManager _windowManager;
        private readonly GameData _gameData = new();
        private PlayerData _playerData;
        private IStaticStorageService StaticStorageService { get; }
        private IDynamicStorageService DynamicStorageService { get; }

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService,
            WindowManager windowManager)
        {
            StaticStorageService = staticStorageService;
            DynamicStorageService = dynamicStorageService;
            _windowManager = windowManager;
        }

        public async UniTask GetGameData() =>
            await StaticStorageService.Download(GAME_DATA_KEY, data =>
            {
                if (data is null)
                    throw new Exception("File is not found");

                _gameData.MaxLevel = data.MaxLevel;
                _gameData.MaxEnergy = data.MaxEnergy;
                Debug.Log(_gameData);
                GameDataLoaded?.Invoke(_gameData);
            });

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

            _playerData.DifferenceLastPlay = CalculateDifferenceLastPlayTime();

            PlayerDataLoaded?.Invoke(_playerData);

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

            PlayerDataLoaded?.Invoke(_playerData);
        }

        public long CalculateDifferenceLastPlayTime()
        {
            var data = PlayerPrefs.GetString(PLAYER_ENTERED, DateTime.Now.ToString());
            if (DateTime.TryParse(data, out var dateTime))
            {
                var delta = DateTime.Now - dateTime;
                Debug.Log($"Difference between last play: {delta.TotalMinutes:N} minutes");
                return (long)delta.TotalMinutes;
            }

            throw new Exception("Error while parsing last play");
        }

        public void SaveQuitTime()
        {
            PlayerPrefs.SetString(PLAYER_ENTERED, DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
    }
}