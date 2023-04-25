using UnityEngine;
using UnityEngine.UI;

class WindowCommon : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    public void Close()
    {
        DestroyImmediate(FindAnyObjectByType<WindowCommon>().gameObject);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}