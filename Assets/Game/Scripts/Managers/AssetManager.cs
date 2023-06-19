using TMPro;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static GameObject GetWindowPrefab(string windowName)
    {
        GameObject windowGO = Resources.Load(windowName) as GameObject;
        GameObject window = Instantiate(windowGO, windowGO.transform.position, Quaternion.identity);
        return window;
    }

    public static GameObject GetDialogBoxPrefab()
    {
        return GetWindowPrefab("Dialog Box");
    }
}
