using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets._Project.Scripts.Storage.Static;
using Game.Scripts.Storage;
using ResourceService;
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

        private readonly IStaticStorageService _staticStorageService;
        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly ResourceManager _resourceManager;
        private readonly LevelManager _levelManager;

        private DynamicPlayerData _dynamicPlayerData;
        private readonly StaticPlayerData _staticPlayerData = new();
        public readonly ShopData ShopData;

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService,
            ResourceManager resourceManager, LevelManager levelManager)
        {
            _staticStorageService = staticStorageService;
            _dynamicStorageService = dynamicStorageService;
            _resourceManager = resourceManager;
            _levelManager = levelManager;
            ShopData = new ShopData();
        }

        public async Task SetName(string newName)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playername", newName },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
                { "action", "changename" },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _dynamicPlayerData.Name = newName;
                    Debug.Log("Renaming Successfully");
                }
                else
                {
                    Debug.Log("Error while renaming");
                }
            });
        }

        public void SetLevel(int level)
        {
            _levelManager.SetCurrentLevel(level);
            //_dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData, b => Print("Level saved successfully!"));
        }

        public void AddSoftCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.SoftCurrency, amount);
            //SaveSoftCurrency();
        }

        public void AddHardCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.HardCurrency, amount);
            //SaveHardCurrency();
        }

        /*
        private void SaveSoftCurrency()
        {
            _dynamicData.AmountSoftResources = GetSoftCurrencyAmount();
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Soft Currency saved successfully!"));
        }

        private void SaveHardCurrency()
        {
            _dynamicData.AmountHardResources = GetHardCurrencyAmount();
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Hard Currency saved successfully!"));
        }

        private void SaveCurrentLevel()
        {
            _dynamicData.CurrentLevel = _levelManager.CurrentLevel;
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Current Level saved successfully!"));

        }
        */

        public int GetCurrentLevel() => _levelManager.GetCurrentLevel();

        public async Task DownloadMaxLevel() =>
            await _staticStorageService.Download(MAX_LEVEL_DATA_KEY, data =>
            {
                if (data is null)
                    throw new Exception("File is not found");

                Debug.Log("MaxLevel: " + data.MaxLevel);
                _staticPlayerData.MaxLevel = data.MaxLevel;
            });

        public async Task<int> GetMaxLevel()
        {
            if (_staticPlayerData is { MaxLevel: > 0 })
                return _staticPlayerData.MaxLevel;

            await DownloadMaxLevel();

            return _staticPlayerData.MaxLevel;
        }

        public int GetSoftCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.SoftCurrency);

        public int GetHardCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.HardCurrency);

        public string GetName() => _dynamicPlayerData.Name;

        public async Task GetDynamicData()
        {
            var downloadParams = new Dictionary<string, string>
            {
                { "action", DYNAMIC_USER_DATA_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Download(downloadParams, Callback);
        }

        private async void Callback(DynamicPlayerData data)
        {
            if (data is not null)
            {
                _dynamicPlayerData = data;
                if (data.Name == DEFAULT_PLAYER_NAME)
                    await SetName("Player " + SystemPlayerData.Instance.uid);

                Debug.Log(data.ToString());
            }
            else
            {
                throw new Exception("dynamic data is null");
            }
        }
    }
}