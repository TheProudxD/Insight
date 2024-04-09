using System;
using Storage;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class AssetManager
    {
        private const string LEVEL_REWARD_WINDOW_KEY = "Level Result";
        private const string SETTINGS_WINDOW_KEY = "Settings";
        private const string PAUSE_WINDOW_KEY = "Pause";
        private const string INVENTORY_WINDOW_KEY = "Inventory";
        private const string EXIT_WINDOW_KEY = "Exit";
        
        public bool DialogWindowAlreadySpawned { get; private set; }
        
        private GameObject GetWindowPrefab(string windowName, Transform parent = null)
        {
            var windowGO = Resources.Load(@"Windows\"+windowName) as GameObject;
            if (windowGO == null)
                throw new Exception($"{windowName} is not in Resources");
            
            var window = Object.Instantiate(windowGO, windowGO.transform.localPosition, Quaternion.identity,
                parent: parent);

            window.transform.localPosition = Vector3.zero;
            return window;
        }

        public GameObject GetDialogBox()
        {
            DialogWindowAlreadySpawned = true;
            return GetWindowPrefab("Dialog Box");
        }
        
        public LevelRewardWindow GetLevelRewardWindow()
        {
            DialogWindowAlreadySpawned = true;
            return GetWindowPrefab(LEVEL_REWARD_WINDOW_KEY).GetComponent<LevelRewardWindow>();
        }

        public PauseWindow GetPauseWindow() => GetWindowPrefab(PAUSE_WINDOW_KEY).GetComponent<PauseWindow>();
        
        public SettingsWindow GetSettingsWindow() => GetWindowPrefab(SETTINGS_WINDOW_KEY).GetComponent<SettingsWindow>();

        public InventoryWindow GetInventoryWindow()=>GetWindowPrefab(INVENTORY_WINDOW_KEY).GetComponent<InventoryWindow>();

        public ExitWindow GetExitWindow()=>GetWindowPrefab(EXIT_WINDOW_KEY).GetComponent<ExitWindow>();

    }
}