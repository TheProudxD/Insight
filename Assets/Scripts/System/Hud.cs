using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider, _manaSlider;
    [SerializeField] private GameObject _settingsWindow, _menuUI;
    private bool _isFullScreen, _isPause, _isInMenu;

    public static Hud Instance;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _isInMenu = true;
        }
    }
    private void Update()
    {
        SetToPauseMode();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DecreaseBar(_hpSlider);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseBar(_hpSlider);
        }
    }

    private void EnablePauseMode()
    {
        _isPause = true;
        Time.timeScale = 0;
        WindowManager.TryShow(WindowType.Pause);
    }
    private void DisablePauseMode()
    {
        _isPause = false;
        Time.timeScale = 1;
        WindowManager.TryClose(WindowType.Pause);
    }
    private void SetToPauseMode()
    {
        if (!_isPause && Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePauseMode();
        }
        else if (_isPause && Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePauseMode();
        }
    }
    public void RestartGame()
    {
        DisablePauseMode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenSettingsWindow()
    {
        _isInMenu = false;
        if (!_menuUI.IsUnityNull()) _menuUI.SetActive(false);
        WindowManager.TryShow(WindowType.Settings);
        _settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        if (_isPause) WindowManager.TryClose(WindowType.Settings);
        _settingsWindow.SetActive(false);
    }
    public void OpenMenu()
    {
        _isInMenu = true;
        if (!_menuUI.IsUnityNull()) _menuUI.SetActive(false);
        DisablePauseMode();
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
