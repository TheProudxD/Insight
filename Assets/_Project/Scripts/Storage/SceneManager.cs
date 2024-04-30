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
        public event Action<Scenes> LevelChanged;

        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly WindowManager _windowManager;

        private int _currentLevel; // max level passed by the player - by default
        private PlayerData _playerData;
        private readonly int _minLevel;

        public int CurrentLevel => _currentLevel;

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
            _currentLevel = playerData.CurrentLevel;
            if (_currentLevel < _minLevel)
            {
                Debug.LogError($"Level must be more than {_minLevel}, but was " + _currentLevel);
                _currentLevel = _minLevel;
                Save(_currentLevel);
            }

            _playerData = playerData;
        }

        private int GetNextLevelId()
        {
            var buildId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

            return buildId switch
            {
                (int)Scenes.Menu => (int)Scenes.Lobby,
                (int)Scenes.Lobby => CurrentLevel,
                _ => CurrentLevel + 1
            };
        }

        private async void Save(int newLevel)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playerlevel", newLevel.ToString() },
                { "action", "changelevel" },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.CurrentLevel = newLevel;
                    _currentLevel = newLevel;
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

            if (newLevel > (int)Scenes.Lobby && newLevel > _currentLevel)
            {
                Save(newLevel);
            }

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > (int)Scenes.Lobby)
            {
                LoadScene(Scenes.Lobby);
                var levelRewardWindow = _windowManager.ShowLevelRewardWindow();
                levelRewardWindow.DisplayReward();
                LevelChanged?.Invoke(Scenes.Lobby);
            }
            else
            {
                LoadScene((Scenes)newLevel);
                LevelChanged?.Invoke((Scenes)newLevel);
            }
        }

        public void LoadScene(Scenes scene)
        {
            LevelChanged?.Invoke(scene);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
    }
}