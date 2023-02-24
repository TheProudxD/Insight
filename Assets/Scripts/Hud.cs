using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    [SerializeField] private GameObject _settingsWindow, _pauseMenu, _menuUI;
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
            _isInMenu=true;
        }
    }
    private void Update()
    {
        SetToPauseMode();
    }

    private void EnablePauseMode()
    {
        _isPause = true;
        Time.timeScale = 0;
        if (!_pauseMenu.IsUnityNull()) _pauseMenu.SetActive(_isPause);
    }
    private void DisablePauseMode()
    {
        _isPause = false;
        Time.timeScale = 1;
        if (!_pauseMenu.IsUnityNull()) _pauseMenu.SetActive(_isPause);
    }
    private void SetToPauseMode()
    {
        if (!_isPause && Input.GetKeyDown(KeyCode.Escape) && !_pauseMenu.activeInHierarchy)
        {
            EnablePauseMode();
        }
        else if (_isPause && Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.activeInHierarchy)
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
        if (!_pauseMenu.IsUnityNull()) _pauseMenu.SetActive(false);
        _settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        if (_isPause && !_pauseMenu.IsUnityNull()) _pauseMenu.SetActive(true);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
    public void FullScreenToggle()
    {
        _isFullScreen = !_isFullScreen;
        Screen.fullScreen = _isFullScreen;
    }
}
