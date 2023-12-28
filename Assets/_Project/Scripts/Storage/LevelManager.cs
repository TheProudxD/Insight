using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Storage
{
    public class LevelManager
    {
        private int _currentLevel;

        public void SetCurrentLevel(int level)
        {
            if (level < 0)
                throw new ArgumentException(nameof(level));
            _currentLevel = level;
            SceneManager.LoadScene(_currentLevel);
        }

        public int GetCurrentLevel()
        {
            /*
            switch (CurrentLevel)
            {
                case < 0:
                    throw new InvalidDataException("Кто поставил отрицательный уровень?");
                case < 2:
                    SetCurrentLevel(CurrentLevel);
                    break;
            }
            */
            return _currentLevel;
        }
    }
}