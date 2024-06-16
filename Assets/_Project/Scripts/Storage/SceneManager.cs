using Storage.Static;
using StorageService;
using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Storage
{
    public class SceneManager
    {
        public const string CHANGE_LEVEL_KEY = "changelevel";
        public event Action<Scene> LevelChanged;

        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly WindowManager _windowManager;

        private PlayerData _playerData;
        private GameData _gameData;
        private readonly int _minLevel;

        public int MaxPassedLevel { get; private set; }

        public int CurrentLevel => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        public Scene CurrentScene => (Scene)CurrentLevel;

        private SceneManager(IDynamicStorageService dynamicStorageService, DataManager dataManager,
            WindowManager windowManager)
        {
            _dynamicStorageService = dynamicStorageService;
            _windowManager = windowManager;
            _minLevel = (int)Scene.LovelyHome;
            dataManager.PlayerDataLoaded += Initialize;
            dataManager.GameDataLoaded += Initialize;
        }

        private void Initialize(PlayerData playerData)
        {
            MaxPassedLevel = playerData.MaxPassedLevel;
            if (MaxPassedLevel < _minLevel)
            {
                Debug.LogError($"Level must be more than {_minLevel}, but was " + MaxPassedLevel);
                MaxPassedLevel = _minLevel;
                Save(MaxPassedLevel);
            }

            _playerData = playerData;
        }

        private void Initialize(GameData gameData) => _gameData = gameData;

        private int GetNextLevelId() => CurrentLevel + 1;

        private async void Save(int newLevel)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playerlevel", newLevel.ToString() },
                { "action", CHANGE_LEVEL_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.MaxPassedLevel = newLevel;
                    MaxPassedLevel = newLevel;
                    Debug.Log($"New level saved Successfully to {newLevel}");
                }
                else
                {
                    Debug.Log("Error while saving level");
                }
            });
        }

        public void StartNextLevel()
        {
            var newLevel = GetNextLevelId();

            if (newLevel > (int)Scene.Lobby && newLevel > MaxPassedLevel)
            {
                Save(newLevel);
            }

            if (CurrentLevel > (int)Scene.Lobby)
            {
                LoadScene(Scene.Lobby);
                var levelRewardWindow = _windowManager.ShowLevelRewardWindow();
                levelRewardWindow.Display(LevelResultType.Successful);
            }
            else
            {
                LoadScene((Scene)newLevel);
            }
        }

        public void LoadScene(Scene scene)
        {
            LevelChanged?.Invoke(scene);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }

        public void RestartLevel() => LoadScene((Scene)CurrentLevel);

        public int GetLevelId(Scene scene) => (int)scene - ((int)Scene.LovelyHome - (int)Scene.Downloader);
    }
}