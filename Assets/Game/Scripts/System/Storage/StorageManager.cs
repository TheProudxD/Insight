using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StorageService
{
    public class StorageManager 
    {
        private const string GAME_DATA_KEY = "AllData";
        //private const string SOFT_CURRENCY_DATA_KEY = "SoftCurrency";
        //private const string HARD_CURRENCY_DATA_KEY = "HardCurrency";
        private const string LOBBY_LOCATION = "Lobby";
        private IStorageService _storageService;
        private StorageData storageData;

        public StorageManager(IStorageService storageService)
        {
            _storageService = storageService;
            storageData = new StorageData();
            if (GetCurrentLevel() < 2)
                SetLevel(2);
        }

        public async Task LoadFiles()
        {
            await _storageService?.Load<StorageData>(GAME_DATA_KEY, data =>
            {
                Print($"All data loaded successfully!");
                if (data is not null)
                    storageData = data;
                else
                    throw new Exception("File is not found");
            });
        }

        private void SetLevel(int level)
        {
            storageData.Level = level;
            _storageService.Save(GAME_DATA_KEY, storageData, b => Print("Level data saved successfully!" ));
        }
        public void SaveSoftCurrency(int amount)
        {
            storageData.AmountSoftResources = amount;
            _storageService.Save(GAME_DATA_KEY, storageData, b => Print("Soft Currency saved successfully!"));
        }
        public void SaveHardCurrency(int amount)
        {
            storageData.AmountHardResources = amount;
            _storageService.Save(GAME_DATA_KEY, storageData, b => Print("Hard Currency saved successfully!"));
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
                currentLevel++;

            SetLevel(currentLevel);
        }

        public int GetCurrentLevel() => storageData.Level;

        public int GetSoftCurrency() => storageData.AmountSoftResources;

        public int GetHardCurrency() => storageData.AmountHardResources;

        public static void Print(string str) => Debug.Log(str);
    }
}