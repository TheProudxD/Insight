using System.Collections.Generic;
using UnityEngine;

public enum WindowType
{
    Pause,
    Settings,
    Inventory,
    Exit
}

public class WindowManager : MonoBehaviour
{
    private SettingsWindow _settingsWindow;
    private Dictionary<WindowType, WindowCommon> _allWindows = new Dictionary<WindowType, WindowCommon>();
    
    private void Awake()
    {
        _allWindows.Clear();
    }

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

    public WindowCommon Create(WindowType windowType)
    {
        WindowCommon windowObject = AssetManager.GetWindowPrefab(windowType.ToString()).GetComponent<WindowCommon>();
        return windowObject;
    }

    public void TryShow(WindowType windowType)
    {
        if (!_allWindows.ContainsKey(windowType))
        {
            WindowCommon window = Create(windowType);
            _allWindows[windowType] = window;
            window.Show();
        }
        else
        {
            print("Открытие уже открытого окна.");
        }
    }

    public void TryClose(WindowType windowType)
    {
        if (_allWindows.ContainsKey(windowType))
        {
            _allWindows[windowType].Close();
            Destroy(_allWindows[windowType]);
            _allWindows.Remove(windowType);
        }
        else
        {
            print("Закрытие уже закрытого окна.");
        }
    }
}