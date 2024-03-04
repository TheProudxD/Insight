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
        [Inject] private LevelRewardSystem _levelRewardSystem;

        private int _currentLevel; // max level passed by the player by default
        private PlayerData _playerData;

		public int CurrentLevel => _currentLevel;

		[Inject]
		private LevelManager(IDynamicStorageService dynamicStorageService)
		{
			_dynamicStorageService = dynamicStorageService;
		}

		public void Initialize(int currentLevel, PlayerData playerData)
        {
            if (currentLevel <= (int)Levels.Lobby)
            {
                throw new ArgumentException("Level must be more than 3, but was " + CurrentLevel);
			}

            _currentLevel = currentLevel;
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
            
            LevelChanged?.Invoke((Levels)newLevel);
            
            if (newLevel-1 > (int)Levels.Lobby)
                _levelRewardSystem.GetReward();
            else
                SceneManager.LoadScene(newLevel);
        }
    }
}