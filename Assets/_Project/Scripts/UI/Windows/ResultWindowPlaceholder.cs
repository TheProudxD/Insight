using Extensions;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Storage
{
    public abstract class ResultWindowPlaceholder : MonoBehaviour
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

        protected void Close() => _levelResultWindow.gameObject.SetActive(false);
        
        protected void Show() => _levelResultWindow.gameObject.SetActive(true);

        protected virtual void OnEnable()
        {
            RestartButton.Add(RestartLevel);
            ReturnToLobbyButton.Add(ReturnToLobby);
            _currentScoreText.SetText(CountCurrentScore().ToString());
            _highScoreText.SetText($"Best score: {CountHighScore()}");
        }

        protected virtual void OnDisable()
        {
            RestartButton.Remove(RestartLevel);
            ReturnToLobbyButton.Remove(ReturnToLobby);
        }
    }
}