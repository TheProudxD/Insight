using System;
using System.Collections.Generic;
using Assets._Project.Scripts.Storage.Static;
using StorageService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Storage
{
    public class LevelManager
    {
        public event Action<Levels> LevelChanged;

        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly LevelRewardSystem _levelRewardSystem;

        private int _currentLevel; // max level passed by the player by default
        private PlayerData _playerData;
        private readonly int _minLevel;

        public int CurrentLevel => _currentLevel;

        private LevelManager(IDynamicStorageService dynamicStorageService, DataManager dataManager,LevelRewardSystem levelRewardSystem)
        {
            _dynamicStorageService = dynamicStorageService;
            
            _levelRewardSystem = levelRewardSystem;
            _minLevel = (int)Levels.Lobby + 1;
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
            var buildId = SceneManager.GetActiveScene().buildIndex;

            return buildId switch
            {
                (int)Levels.Menu => (int)Levels.Lobby,
                (int)Levels.Lobby => CurrentLevel,
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

            if (newLevel > (int)Levels.Lobby && newLevel > _currentLevel)
            {
                Save(newLevel);
            }

            if (SceneManager.GetActiveScene().buildIndex > (int)Levels.Lobby)
            {
                SceneManager.LoadScene((int)Levels.Lobby);
                _levelRewardSystem.GetReward();
                LevelChanged?.Invoke(Levels.Lobby);
            }
            else
            {
                SceneManager.LoadScene(newLevel);
                LevelChanged?.Invoke((Levels)newLevel);
            }
        }
    }
}