using System;
using System.IO;

namespace Game.Scripts.Storage
{
    public class LevelManager
    {
        public int CurrentLevel { get; private set; }

        public void SetCurrentLevel(int level)
        {
            if (level <= 0)
                throw new ArgumentException(nameof(level));
            CurrentLevel = level;
        }

        public int GetCurrentLevel()
        {
            switch (CurrentLevel)
            {
                case < 0:
                    throw new InvalidDataException("Кто поставил отрицательный уровень?");
                case < 2:
                    SetCurrentLevel(2);
                    break;
            }

            return CurrentLevel;
        }
    }
}