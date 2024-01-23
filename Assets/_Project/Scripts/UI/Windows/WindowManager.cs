using System.Collections.Generic;
using UI;
using UnityEngine;

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

        private WindowCommon Create(WindowType windowType)
        {
            var windowObject = _assetManager.GetWindowPrefab(windowType.ToString()).GetComponent<WindowCommon>();
            return windowObject;
        }

        private void TryShow(WindowType windowType)
        {
            if (!_openedWindows.ContainsKey(windowType))
            {
                var window = Create(windowType);
                _openedWindows[windowType] = window;
                window.Show();
            }
            else
            {
                Debug.LogError("Открытие уже открытого окна.");
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