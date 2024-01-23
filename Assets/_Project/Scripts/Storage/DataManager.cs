using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets._Project.Scripts.Storage.Static;
using Storage;
using ResourceService;
using UI;
using UI.Shop.Data;
using UnityEngine;

namespace StorageService
{
    public class DataManager
    {
        public const string REGISTRY_DATA_KEY = "registry";
        public const string MAX_LEVEL_DATA_KEY = "maxleveldata";
        public const string DYNAMIC_USER_DATA_KEY = "userdata";
        private const string DEFAULT_PLAYER_NAME = "Player";

        public ShopData ShopData { get; private set; }
        private readonly ResourceManager _resourceManager;
        private readonly LevelManager _levelManager;
        private readonly Hud _hud;
        private readonly GameData _gameData = new();
        private PlayerData _playerData = new();
        private IStaticStorageService StaticStorageService { get; set; }
        private IDynamicStorageService DynamicStorageService { get; set; }

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService,
            ResourceManager resourceManager, LevelManager levelManager, Hud hud)
        {
            StaticStorageService = staticStorageService;
            DynamicStorageService = dynamicStorageService;
            _resourceManager = resourceManager;
            _levelManager = levelManager;
            _hud = hud;
        }

        public async Task SetName(string newName)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playername", newName },
                { "action", "changename" },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.Name = newName;
                    Debug.Log("Renaming Successfully");
                }
                else
                {
                    Debug.Log("Error while renaming");
                }
            });
        }

        public async Task DownloadMaxLevel() =>
            await StaticStorageService.Download(MAX_LEVEL_DATA_KEY, data =>
            {
                if (data is null)
                    throw new Exception("File is not found");

                Debug.Log("MaxLevel: " + data.MaxLevel);
                _gameData.MaxLevel = data.MaxLevel;
            });

        public async Task<int> GetMaxLevel()
        {
            if (_gameData is { MaxLevel: > 0 })
                return _gameData.MaxLevel;

            await DownloadMaxLevel();

            return _gameData.MaxLevel;
        }

        public string GetName() => _playerData.Name;

        public async Task GetDynamicData()
        {
            var downloadParams = new Dictionary<string, string>
            {
                { "action", DYNAMIC_USER_DATA_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Download(downloadParams, Callback);
        }

        private async void Callback(PlayerData data)
        {
            if (data is not null)
            {
                _playerData = data;
                if (data.Name == DEFAULT_PLAYER_NAME)
                    await SetName("Player " + SystemPlayerData.Instance.uid);

                _resourceManager.Initialize(_playerData.AmountSoftResources,
                    _playerData.AmountHardResources, _playerData);
                _levelManager.Initialize(data.CurrentLevel, _playerData);
                ShopData = new ShopData();
                _hud.SetPlayerNickname(_playerData.Name);

                Debug.Log(data.ToString());
            }
            else
            {
                throw new Exception("dynamic data is null");
            }
        }
    }
}