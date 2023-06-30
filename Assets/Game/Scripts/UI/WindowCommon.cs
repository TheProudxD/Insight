using UnityEngine;

public class WindowCommon : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}