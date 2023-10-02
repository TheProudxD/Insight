using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StorageService
{
    public class StorageManager : MonoBehaviour
    {
        private const string GAME_DATA_KEY = "AllData";
        //private const string SOFT_CURRENCY_DATA_KEY = "SoftCurrency";
        //private const string HARD_CURRENCY_DATA_KEY = "HardCurrency";
        private const string LOBBY_LOCATION = "Lobby";
        private static IStorageService _storageService;
        private static StorageData storageData;
        public static int GameLevel { get; private set; }

        private void Awake()
        {
            _storageService = new JsonToFileStorageService();
            storageData = new StorageData();
            if (GetCurrentLevel() < 2)
                SetLevel(2);
        }

        private static int GetCurrentLevel()
        {
            int value = default;
            _storageService?.Load<StorageData>(GAME_DATA_KEY, data =>
            {
                print($"Level data loaded successfully!");
                if (data is not null) value = data.Level;
            });
            return value;
        }

        private static void SetLevel(int level)
        {
            storageData.Level = level;
            _storageService.Save(GAME_DATA_KEY, storageData, b => print("Level data saved successfully!" ));
        }
        public static void SaveSoftCurrency(int amount)
        {
            storageData.AmountSoftResources = amount;
            _storageService.Save(GAME_DATA_KEY, storageData, b => print("Soft Currency saved successfully!"));
        }
        public static void SaveHardCurrency(int amount)
        {
            storageData.AmountHardResources = amount;
            _storageService.Save(GAME_DATA_KEY, storageData, b => print("Hard Currency saved successfully!"));
        }

        public static void SaveLevelData()
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

        public static int GetSoftCurrency()
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

        public static int GetHardCurrency()
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
    }
}