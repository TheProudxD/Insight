using UnityEngine;
public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool _isPause;
    void Update()
    {
        SetToPauseMode();
    }
    private void SetToPauseMode()
    {
        if (!_isPause && Input.GetKeyDown(KeyCode.Escape)) _isPause = true;
        else if (_isPause && Input.GetKeyDown(KeyCode.Escape)) _isPause = false;
        PauseMenu.gameObject.SetActive(_isPause);
    }
}