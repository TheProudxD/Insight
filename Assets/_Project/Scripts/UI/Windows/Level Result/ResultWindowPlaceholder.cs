using Extensions;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Storage
{
    public abstract class ResultWindowPlaceholder : CommonWindow
    {
        [Inject] protected SceneManager SceneManager;
        [Inject] protected WindowManager WindowManager;

        [SerializeField] private LevelResultWindow _levelResultWindow;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] protected Button ReturnToLobbyButton;
        [SerializeField] protected Button RestartButton;

        private void RestartLevel()
        {
            SceneManager.RestartLevel();
            Close();
        }

        private void ReturnToLobby()
        {
            var exitWindow = WindowManager.ShowExitWindow();
            exitWindow.OnExit(() =>
            {
                SceneManager.LoadScene(Scene.Lobby);
                exitWindow.Close();
            });
            Close();
        }
        
        protected int CountCurrentScore() => 100;

        protected int CountHighScore() => 1000;

        protected new virtual void OnEnable()
        {
            RestartButton.Add(RestartLevel);
            RestartButton.Add(AudioPlayer.PlayButtonSound);
            ReturnToLobbyButton.Add(ReturnToLobby);
            ReturnToLobbyButton.Add(AudioPlayer.PlayButtonSound);
            
            _currentScoreText.SetText(CountCurrentScore().ToString());
            _highScoreText.SetText($"Best score: {CountHighScore()}");
        }

        protected new virtual void OnDisable()
        {
            RestartButton.Remove(RestartLevel);
            RestartButton.Remove(AudioPlayer.PlayButtonSound);
            ReturnToLobbyButton.Remove(ReturnToLobby);
            ReturnToLobbyButton.Remove(AudioPlayer.PlayButtonSound);
        }
    }
}