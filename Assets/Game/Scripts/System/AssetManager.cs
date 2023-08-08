using System;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static bool DialogAlreadySpawned { get; private set; }
    public static GameObject GetWindowPrefab(string windowName)
    {
        var windowGO = Resources.Load(windowName) as GameObject;
        var window = Instantiate(windowGO, windowGO.transform.position, Quaternion.identity);
        return window;
    }

    public static GameObject GetDialogBoxPrefab()
    {
        DialogAlreadySpawned = true;
        return GetWindowPrefab("Dialog Box");
    }

    private void OnDestroy()
    {
        DialogAlreadySpawned=false;
    }
}