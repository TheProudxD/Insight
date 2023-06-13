using UnityEngine;

class WindowCommon : MonoBehaviour
{
    public void Close() => Destroy(gameObject);

    public void Show() => gameObject.SetActive(true);
}