using UnityEngine;
using System.Linq;
using UnityEngine.UI;

class WindowCommon : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    public void Close()
    {
        DestroyImmediate(FindObjectOfType<WindowCommon>());
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}