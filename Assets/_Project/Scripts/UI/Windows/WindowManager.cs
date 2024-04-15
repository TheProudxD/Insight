using System;
using System.Collections.Generic;
using Storage;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class WindowManager
    {
        private readonly Dictionary<WindowType, CommonWindow> _openedWindows = new();
        private readonly AssetManager _assetManager;

        private WindowManager(AssetManager assetManager) => _assetManager = assetManager;

        private CommonWindow TryShow(WindowType windowType)
        {
            if (!_openedWindows.ContainsKey(windowType))
            {
                var window = _assetManager.GetWindowPrefab(windowType);

                _openedWindows[windowType] = window;
                window.Show();
            }
            else
            {
                //TryClose(windowType);
                Debug.LogError("Открытие уже открытого окна.");
            }

            return _openedWindows[windowType];
        }

        private void TryClose(WindowType windowType)
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

        public DialogBox ShowDialogBox() => (DialogBox)TryShow(WindowType.Dialog);
        public void CloseDialogBox() => TryClose(WindowType.Dialog);

        public LevelRewardCommonWindow ShowLevelRewardWindow() => (LevelRewardCommonWindow)TryShow(WindowType.LevelReward);
        public void CloseLevelRewardWindow() => TryClose(WindowType.LevelReward);

        public PauseCommonWindow ShowPauseWindow() => (PauseCommonWindow)TryShow(WindowType.Pause);
        public void ClosePauseWindow() => TryClose(WindowType.Pause);

        public SettingsCommonWindow ShowSettingsWindow() => (SettingsCommonWindow)TryShow(WindowType.Settings);
        public void CloseSettingsWindow() => TryClose(WindowType.Settings);

        public InventoryWindow ShowInventoryWindow() => (InventoryWindow)TryShow(WindowType.Inventory);
        public void CloseInventoryWindow() => TryClose(WindowType.Inventory);

        public ExitCommonWindow ShowExitWindow() => (ExitCommonWindow)TryShow(WindowType.Exit);
        public void CloseExitWindow() => TryClose(WindowType.Exit);

        public LevelSelectWindow ShowLevelSelectWindow()=> (LevelSelectWindow)TryShow(WindowType.LevelSelect);
        public void CloseLevelSelectWindow() => TryClose(WindowType.LevelSelect);
    }
}