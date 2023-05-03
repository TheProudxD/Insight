using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static GameObject GetWindowPrefab(string windowName)
    {
        var windowGO = Resources.Load(windowName) as GameObject;
        Instantiate(windowGO, windowGO.transform.position, Quaternion.identity);
        return windowGO;
    }
}
