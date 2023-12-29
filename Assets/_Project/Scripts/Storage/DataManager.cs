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

        public IStaticStorageService StaticStorageService { get; private set; }
        public IDynamicStorageService DynamicStorageService { get; private set; }
        public ResourceManager ResourceManager { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public ShopData ShopData { get; private set; }

        private DynamicPlayerData _dynamicPlayerData = new();
        private readonly StaticPlayerData _staticPlayerData = new();

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService)
        {
            StaticStorageService = staticStorageService;
            DynamicStorageService = dynamicStorageService;
            LevelManager = new LevelManager();
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

            await DynamicStorageService.Upload(uploadParams, result =>
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
            LevelManager.SetCurrentLevel(level);
            //_dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData, b => Print("Level saved successfully!"));
        }

        /*
         public void AddSoftCurrency(int amount)
        {
            ResourceManager.AddResource(ResourceType.SoftCurrency, amount);
            //SaveSoftCurrency();
        }
        
        public void AddHardCurrency(int amount)
        {
            ResourceManager.AddResource(ResourceType.HardCurrency, amount);
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
            
        public int GetSoftCurrencyAmount() => ResourceManager.GetResourceValue(ResourceType.SoftCurrency);
          
        public int GetHardCurrencyAmount() => ResourceManager.GetResourceValue(ResourceType.HardCurrency);
        */

        public async Task DownloadMaxLevel() =>
            await StaticStorageService.Download(MAX_LEVEL_DATA_KEY, data =>
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

        public string GetName() => _dynamicPlayerData.Name;

        public async Task GetDynamicData()
        {
            var downloadParams = new Dictionary<string, string>
            {
                { "action", DYNAMIC_USER_DATA_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Download(downloadParams, Callback);
        }

        private async void Callback(DynamicPlayerData data)
        {
            if (data is not null)
            {
                _dynamicPlayerData = data;
                if (data.Name == DEFAULT_PLAYER_NAME)
                    await SetName("Player " + SystemPlayerData.Instance.uid);

                ResourceManager = new ResourceManager(_dynamicPlayerData.AmountSoftResources,
                    _dynamicPlayerData.AmountHardResources);

                Debug.Log(data.ToString());
            }
            else
            {
                throw new Exception("dynamic data is null");
            }
        }
    }
}