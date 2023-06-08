using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static GameObject GetWindowPrefab(string windowName)
    {
        var windowGO = Resources.Load(windowName) as GameObject;
        var window = Instantiate(windowGO, windowGO.transform.position, Quaternion.identity, GameManager.Instance.WindowCanvas);
        return window;
    }
}
