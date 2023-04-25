using System.Collections.Generic;
using UnityEngine;

public enum WindowType
{
    Pause,
    Settings
}
public class WindowManager : MonoBehaviour
{
    private static Dictionary<WindowType, WindowCommon> _allWindows = new Dictionary<WindowType, WindowCommon>();

    private void Awake()
    {
        _allWindows.Clear();
    }
    public static void Create(WindowType windowType)
    {
        var windowObject = AssetManager.GetWindowPrefab(windowType.ToString()).GetComponent<WindowCommon>();
        _allWindows.Add(windowType, windowObject);
    }
    public static void TryShow(WindowType windowType)
    {
        if (_allWindows.ContainsKey(windowType))
        {
            _allWindows[windowType].Show();
        }
        else
        {
            Create(windowType);
            _allWindows[windowType].Show();
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
            throw new System.Exception("Закрытие уже закрытого окна.");
        }
    }
}