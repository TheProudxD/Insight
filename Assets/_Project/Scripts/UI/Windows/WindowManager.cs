using System.Collections.Generic;
using Storage;
using UI;
using UI.Shop;
using UnityEngine;

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
            }

            _openedWindows[windowType].Show();

            return _openedWindows[windowType];
        }

        private void TryClose(WindowType windowType)
        {
            if (_openedWindows.TryGetValue(windowType, out var window))
            {
                window.Close();
            }
            else
            {
                Debug.LogError("Закрытие не открытого окна.");
            }
        }

        public DialogBox ShowDialogBox() => (DialogBox)TryShow(WindowType.Dialog);
        public void CloseDialogBox() => TryClose(WindowType.Dialog);

        public LevelRewardCommonWindow ShowLevelRewardWindow() =>
            (LevelRewardCommonWindow)TryShow(WindowType.LevelReward);

        public void CloseLevelRewardWindow() => TryClose(WindowType.LevelReward);

        public PauseCommonWindow ShowPauseWindow() => (PauseCommonWindow)TryShow(WindowType.Pause);
        public void ClosePauseWindow() => TryClose(WindowType.Pause);

        public SettingsCommonWindow ShowSettingsWindow() => (SettingsCommonWindow)TryShow(WindowType.Settings);
        public void CloseSettingsWindow() => TryClose(WindowType.Settings);

        public InventoryWindow ShowInventoryWindow() => (InventoryWindow)TryShow(WindowType.Inventory);
        public void CloseInventoryWindow() => TryClose(WindowType.Inventory);

        public ExitCommonWindow ShowExitWindow() => (ExitCommonWindow)TryShow(WindowType.Exit);
        public void CloseExitWindow() => TryClose(WindowType.Exit);

        public LevelSelectWindow ShowLevelSelectWindow() => (LevelSelectWindow)TryShow(WindowType.LevelSelect);
        public void CloseLevelSelectWindow() => TryClose(WindowType.LevelSelect);        
        
        public Shop ShowShopWindow() => (Shop)TryShow(WindowType.Shop);
        public void CloseShopWindow() => TryClose(WindowType.Shop);
    }
}