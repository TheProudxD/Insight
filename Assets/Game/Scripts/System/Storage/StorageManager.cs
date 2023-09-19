using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StorageService
{
    public class StorageManager : MonoBehaviour
    {
        private const string LEVEL_DATA_KEY = "LevelData";
        private const string LOBBY_LOCATION = "Lobby";
        private static IStorageService _storageService;
        public static int GameLevel { get; private set; }

        private void Awake()
        {
            _storageService = new JsonToFileStorageService();

            if (GetCurrentLevel() <= 2)
                SetLevel(2);
        }

        private static int GetCurrentLevel()
        {
            int value = default;
            _storageService?.Load<StorageData>(LEVEL_DATA_KEY, data =>
            {
                print($"Level data loaded successfully!");
                if (data is not null) value = data.Level;
            });
            return value;
        }

        private static void SetLevel(int level)
        {
            var d = new StorageData
            {
                Level = level
            };
            _storageService.Save(LEVEL_DATA_KEY, d, b => { print("Level data saved successfully!"); });
        }

        public static void SaveLevelData()
        {
            if (SceneManager.GetActiveScene().name == LOBBY_LOCATION)
            {
                var currentLevel = GetCurrentLevel();
                if (currentLevel < 0) throw new Exception("Кто поставил отрицательный уровень??");
                GameLevel = currentLevel;
            }
            else
            {
                GameLevel++;
            }

            SetLevel(GameLevel > GetCurrentLevel() ? GameLevel : GetCurrentLevel());
        }
    }
}