using Assets._Project.Scripts.Storage.Static;
using StorageService;
using System;
using System.Collections.Generic;
using System.Threading;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Storage
{
    public class SceneManager
    {
        public const string CHANGE_LEVEL_KEY = "changelevel";
        public event Action<Scenes> LevelChanged;

        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly WindowManager _windowManager;

        private PlayerData _playerData;
        private readonly int _minLevel;

        public int MaxPassedLevel { get; private set; }

        public int CurrentLevel => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        private SceneManager(IDynamicStorageService dynamicStorageService, DataManager dataManager,
            WindowManager windowManager)
        {
            _dynamicStorageService = dynamicStorageService;
            _windowManager = windowManager;
            _minLevel = (int)Scenes.Lobby + 1;
            dataManager.DataLoaded += Initialize;
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

        private int GetNextLevelId()
        {
            var buildId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

            return buildId switch
            {
                (int)Scenes.Menu => (int)Scenes.Lobby,
                (int)Scenes.Lobby => MaxPassedLevel,
                _ => MaxPassedLevel + 1
            };
        }

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

            if (newLevel > (int)Scenes.Lobby && newLevel > MaxPassedLevel)
            {
                Save(newLevel);
            }

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > (int)Scenes.Lobby)
            {
                LoadScene(Scenes.Lobby);
                var levelRewardWindow = _windowManager.ShowLevelRewardWindow();
                levelRewardWindow.DisplayReward();
            }
            else
            {
                LoadScene((Scenes)newLevel);
            }
        }

        public void LoadScene(Scenes scene)
        {
            LevelChanged?.Invoke(scene);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }

        public void RestartLevel() => LoadScene((Scenes)CurrentLevel);
    }
}