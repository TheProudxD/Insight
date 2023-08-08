using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private readonly Dictionary<WindowType, WindowCommon> _allWindows = new();

    private void Awake()
    {
        _allWindows.Clear();
    }

    public WindowCommon Create(WindowType windowType)
    {
        var windowObject = AssetManager.GetWindowPrefab(windowType.ToString()).GetComponent<WindowCommon>();
        return windowObject;
    }

    public void TryShow(WindowType windowType)
    {
        if (!_allWindows.ContainsKey(windowType))
        {
            var window = Create(windowType);
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