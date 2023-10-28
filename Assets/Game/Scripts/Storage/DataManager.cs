using System;
using ResourceService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StorageService
{
    public class DataManager
    {
        public const string JSON_DATA_KEY = "AllData";
        public const string DATABASE_DATA_KEY = "alldata";
        
        private const string LOBBY_LOCATION = "Lobby";
        private readonly IStorageService _storageService;
        private readonly ITransferService _transferService;
        private readonly ResourceManager _resourceManager;
        
        private JSONData _jsonData;
        private DynamicData _dynamicData;

        public DataManager(IStorageService storageService, ITransferService transferService, ResourceManager resourceManager)
        {
            _storageService = storageService;
            _transferService = transferService;
            _dynamicData = new DynamicData();
            _resourceManager = resourceManager;
            if (GetCurrentLevel() < 2)
                SetLevel(2);
        }

        private void SetLevel(int level)
        {
            _dynamicData.CurrentLevel = level;
            _transferService.Save(JSON_DATA_KEY, _dynamicData, b => Print("Level saved successfully!"));
        }

        public void SetSoftCurrency(int amount) => _resourceManager.AddResource(ResourceType.SoftCurrency, amount);
        
        public void SetHardCurrency(int amount) => _resourceManager.AddResource(ResourceType.HardCurrency, amount);

        public void SaveSoftCurrency()
        {
            _dynamicData.AmountSoftResources = GetSoftCurrencyAmount();
            _transferService.Save(JSON_DATA_KEY, _dynamicData, b => Print("Soft Currency saved successfully!"));
        }

        public void SaveHardCurrency()
        {
            _dynamicData.AmountHardResources = GetHardCurrencyAmount();
            _transferService.Save(JSON_DATA_KEY, _dynamicData, b => Print("Hard Currency saved successfully!"));
        }

        public void SaveLevelData()
        {
            var currentLevel = GetCurrentLevel();
            if (SceneManager.GetActiveScene().name == LOBBY_LOCATION)
            {
                if (currentLevel < 0)
                    throw new Exception("Кто поставил отрицательный уровень??");
            }
            else
            {
                currentLevel++;
            }

            SetLevel(currentLevel);
        }

        public int GetCurrentLevel() => _dynamicData.CurrentLevel;
        
        public int GetMaxLevel() => _jsonData.MaxLevel;

        public int GetSoftCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.SoftCurrency);

        public int GetHardCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.HardCurrency);

        public void GetData(JSONData data) => _jsonData = data;
        public void GetData(DynamicData data) => _dynamicData = data;

        private static void Print(string str) => Debug.Log(str);
    }
}