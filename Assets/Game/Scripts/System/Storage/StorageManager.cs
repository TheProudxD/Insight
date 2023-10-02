using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace StorageService
{
    public class StorageManager 
    {
        private const string GAME_DATA_KEY = "AllData";
        //private const string SOFT_CURRENCY_DATA_KEY = "SoftCurrency";
        //private const string HARD_CURRENCY_DATA_KEY = "HardCurrency";
        private const string LOBBY_LOCATION = "Lobby";
        private IStorageService _storageService;
        private static StorageData storageData;
        public static int GameLevel { get; private set; }
        public StorageManager(IStorageService storageService)
        {
            _storageService = storageService;
            storageData = new StorageData();
            if (GetCurrentLevel() < 2)
                SetLevel(2);
        }

        private int GetCurrentLevel()
        {
            int value = default;
            _storageService?.Load<StorageData>(GAME_DATA_KEY, data =>
            {
                Print($"Level data loaded successfully!");
                if (data is not null) value = data.Level;
            });
            return value;
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
            if (SceneManager.GetActiveScene().name == LOBBY_LOCATION)
            {
                var currentLevel = GetCurrentLevel();
                if (currentLevel < 0) 
                    throw new Exception("Кто поставил отрицательный уровень??");
                GameLevel = currentLevel;
            }
            else
            {
                GameLevel++;
            }

            SetLevel(GameLevel > GetCurrentLevel() ? GameLevel : GetCurrentLevel());
        }

        public int GetSoftCurrency()
        {
            int value = default;
            _storageService?.Load<StorageData>(GAME_DATA_KEY, data =>
            {
                //print($"Soft currency loaded successfully!");
                if (data is not null) 
                    value = data.AmountSoftResources;
            });
            return value;
        }

        public int GetHardCurrency()
        {
            int value = default;
            _storageService?.Load<StorageData>(GAME_DATA_KEY, data =>
            {
                //print($"Hard currency loaded successfully!");
                if (data is not null) 
                    value = data.AmountHardResources;
            });
            return value;
        }

        public static void Print(string str) => Debug.Log(str);
    }
}