using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class WindowManager
    {
        private readonly Dictionary<WindowType, WindowCommon> _openedWindows = new();
        private readonly AssetManager _assetManager;

        private WindowManager(AssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        private void TryShow(WindowType windowType)
        {
            if (!_openedWindows.ContainsKey(windowType))
            {
                WindowCommon window = windowType switch
                {
                    WindowType.Pause => _assetManager.GetPauseWindow(),
                    WindowType.Settings => _assetManager.GetSettingsWindow(),
                    WindowType.Inventory => _assetManager.GetInventoryWindow(),
                    WindowType.Exit => _assetManager.GetExitWindow(),
                    WindowType.LevelReward => _assetManager.GetLevelRewardWindow(),
                    _ => throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null)
                };
                
                _openedWindows[windowType] = window;
                window.Show();
            }
            else
            {
                TryClose(windowType);
                //Debug.LogError("Открытие уже открытого окна.");
            }
        }

        public void TryClose(WindowType windowType)
        {
            if (_openedWindows.ContainsKey(windowType))
            {
                _openedWindows[windowType].Close();
                Object.Destroy(_openedWindows[windowType]);
                _openedWindows.Remove(windowType);
            }
            else
            {
                Debug.LogError("Закрытие уже закрытого окна.");
            }
        }

        #region openners

        public void OpenSettingsWindow()
        {
            TryShow(WindowType.Settings);
        }

        public void OpenInventoryWindow()
        {
            TryShow(WindowType.Inventory);
        }

        public void OpenExitWindow()
        {
            TryShow(WindowType.Exit);
        }

        public void OpenPauseWindow()
        {
            TryShow(WindowType.Pause);
        }

        #endregion
    }
}