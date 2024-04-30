using System;
using Storage;
using UI;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Managers
{
    public class AssetManager
    {
        private const string PREFABS_FOLDER = "Windows";
        
        private const string DIALOG_BOX_KEY = "Dialog Box";

        private const string LEVEL_REWARD_WINDOW_KEY = "Level Result";
        private const string SETTINGS_WINDOW_KEY = "Settings";
        private const string PAUSE_WINDOW_KEY = "Pause";
        private const string INVENTORY_WINDOW_KEY = "Inventory";
        private const string EXIT_WINDOW_KEY = "Exit";
        private const string LEVEL_SELECT_WINDOW_KEY = "Level Select";
        private const string SHOP_WINDOW_KEY = "Shop";
        
        public CommonWindow GetWindowPrefab(WindowType windowType, Transform parent = null)
        {
            var windowName = windowType switch
            {
                WindowType.Dialog => DIALOG_BOX_KEY,
                WindowType.LevelReward => LEVEL_REWARD_WINDOW_KEY,
                WindowType.Settings => SETTINGS_WINDOW_KEY,
                WindowType.Pause => PAUSE_WINDOW_KEY,
                WindowType.Inventory => INVENTORY_WINDOW_KEY,
                WindowType.Exit => EXIT_WINDOW_KEY,
                WindowType.LevelSelect => LEVEL_SELECT_WINDOW_KEY,
                WindowType.Shop => SHOP_WINDOW_KEY,
                _ => throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null)
            };

            var windowGO = Resources.Load($@"{PREFABS_FOLDER}/{windowName}") as GameObject;
            if (windowGO == null)
                throw new Exception($@"{windowName} is not in Resources/{PREFABS_FOLDER}");
            
            var window = ProjectContext.Instance.Container.InstantiatePrefab(windowGO, parent);

            window.transform.localPosition = Vector3.zero;
            return window.GetComponent<CommonWindow>();
        }
    }
}