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
    private static Dictionary<WindowType, WindowCommon> _allWindows = new Dictionary<WindowType, WindowCommon>();

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

    public static void Create(WindowType windowType)
    {
        WindowCommon windowObject = AssetManager.GetWindowPrefab(windowType.ToString()).GetComponent<WindowCommon>();
        _allWindows.Add(windowType, windowObject);

        switch (windowType)
        {
            case WindowType.Pause:
                windowObject.GetComponent<PauseWindow>().Show();
                break;
            case WindowType.Settings:
                windowObject.GetComponent<SettingsWindow>().Show();
                break;
            default:
                break;
        }
    }

    public static void TryShow(WindowType windowType)
    {
        if (!_allWindows.ContainsKey(windowType))
        {
            Create(windowType);
        }
        else
        {
            print("Открытие уже открытого окна.");
        }
    }

    public static void TryClose(WindowType windowType)
    {
        if (_allWindows.ContainsKey(windowType))
        {
            _allWindows[windowType].Close();
            _allWindows.Remove(windowType);
        }
        else
        {
            print("Закрытие уже закрытого окна.");
        }
    }
}