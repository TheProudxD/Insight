using System;
using StorageService;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Storage
{
    public enum Levels
    {
        Bootstrap = 0,
        Menu,
        Lobby,
        Home,
        Dungeon
    }

    public class LevelManager
    {
        private int _currentLevel; // max level passed by the player
        private readonly IDynamicStorageService _dynamicStorageService;

        [Inject]
        private LevelManager(IDynamicStorageService dynamicStorageService)
        {
            _dynamicStorageService = dynamicStorageService;
        }

        public void Initialize(int currentLevel)
        {
            if (currentLevel <= (int)Levels.Lobby)
            {
                throw new ArgumentException("Level must be more than 3");
            }

            _currentLevel = currentLevel;
        }

        public void LoadLevel(int level)
        {
            if (level < 0)
            {
                throw new ArgumentException("Negative level " + nameof(level));
            }

            if (SceneManager.GetActiveScene().buildIndex < (int)Levels.Lobby)
            {
                throw new ArgumentException("Changing level on unapproved scene" + nameof(level));
            }

            _currentLevel = level;
            SceneManager.LoadScene(_currentLevel);
        }

        public int GetNextLevelId()
        {
            var buildId = SceneManager.GetActiveScene().buildIndex;

            return buildId switch
            {
                (int)Levels.Bootstrap or (int)Levels.Menu => buildId + 1,
                (int)Levels.Lobby => _currentLevel,
                _ => _currentLevel + 1
            };
        }
    }
}