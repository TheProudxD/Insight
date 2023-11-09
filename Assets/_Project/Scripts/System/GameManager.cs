using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const string LOBBY_LOCATION = "Lobby";
        public static GameManager Instance;

        [SerializeField] private Signal _gameOverSignal;

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

        public void StartGame() => SceneManager.LoadScene(LOBBY_LOCATION);

        public IEnumerator GameOver()
        {
            yield return new WaitForSeconds(1.5f);
            _gameOverSignal.Raise();
        }
    }
}