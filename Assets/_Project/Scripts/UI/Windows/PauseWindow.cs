using Extensions;
using Managers;
using Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseWindow : CommonWindow
    {
        [Inject] private SceneManager _sceneManager;
        [Inject] private WindowManager _windowManager;

        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _returnToLobbyButton;

        private void EnablePauseMode() => Time.timeScale = 0;

        private void DisablePauseMode() => Time.timeScale = 1;

        private void RestartLevel()
        {
            _sceneManager.RestartLevel();
            DisablePauseMode();
            Close();
        }

        private void ReturnToLobby()
        {
            var exitWindow = _windowManager.ShowExitWindow();
            exitWindow.OnExit(() =>
            {
                _sceneManager.LoadScene(Scene.Lobby);
                exitWindow.Close();
            });
            DisablePauseMode();
            Close();
        }

        private void ReturnInGame()
        {
            base.Close();
            DisablePauseMode();
        }

        protected override void OnEnable()
        {
            EnablePauseMode();

            CloseButton.Add(ReturnInGame);
            CloseButton.Add(AudioPlayer.PlayButtonSound);
            _restartLevelButton.Add(RestartLevel);
            _restartLevelButton.Add(AudioPlayer.PlayButtonSound);
            _returnToLobbyButton.Add(ReturnToLobby);
            _returnToLobbyButton.Add(AudioPlayer.PlayButtonSound);
        }

        protected override void OnDisable()
        {
            DisablePauseMode();

            CloseButton.Remove(ReturnInGame);
            CloseButton.Remove(AudioPlayer.PlayButtonSound);
            _restartLevelButton.Remove(RestartLevel);
            _restartLevelButton.Remove(AudioPlayer.PlayButtonSound);
            _returnToLobbyButton.Remove(ReturnToLobby);
            _returnToLobbyButton.Remove(AudioPlayer.PlayButtonSound);
        }
    }
}