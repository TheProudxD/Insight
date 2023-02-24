using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    [SerializeField] private GameObject _settingsWindow, _pauseMenu;
    private bool _isFullScreen, _isPause;

    public static Hud Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        _pauseMenu.SetActive(_isPause);
    }
    private void DisablePauseMode()
    {
        _isPause = false;
        Time.timeScale = 1;
        _pauseMenu.SetActive(_isPause);
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
        _pauseMenu.SetActive(false);
        _settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        if (_isPause) _pauseMenu.SetActive(true);
        _settingsWindow.SetActive(false);
    }
    public void OpenMenu()
    {
        DisablePauseMode();
        SceneManager.LoadScene(0);
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
