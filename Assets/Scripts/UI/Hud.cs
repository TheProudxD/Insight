using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider, _manaSlider;
    [SerializeField] private GameObject _settingsWindow, _menuUI;
    private bool _isFullScreen;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    DecreaseBar(_hpSlider);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    IncreaseBar(_hpSlider);
        //}
    }

    public void OpenSettingsWindow()
    {
        if (!_menuUI.IsUnityNull()) _menuUI.SetActive(false);
        WindowManager.TryShow(WindowType.Settings);
        _settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        if (WindowManager.IsPause) WindowManager.TryClose(WindowType.Settings);
        _settingsWindow.SetActive(false);
    }

    public void OpenMenu()
    {
        if (!_menuUI.IsUnityNull()) _menuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Exit pressed!");
    }
    public void FullScreenToggle()
    {
        _isFullScreen = !_isFullScreen;
        Screen.fullScreen = _isFullScreen;
    }

    private void DecreaseBar(Slider slider)
    {
        if (slider.value > 0)
        {
            slider.GetComponentsInChildren<Image>()[1].enabled = true;
            slider.value -= 10;
        }
        else
        {
            slider.GetComponentsInChildren<Image>()[1].enabled = false;
        }
    }

    private void IncreaseBar(Slider slider)
    {
        if (!slider.GetComponentsInChildren<Image>()[1].enabled)
            slider.GetComponentsInChildren<Image>()[1].enabled = true;
        else
            slider.value += 10;
    }
}
