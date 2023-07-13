using System.Collections;
using Game.Scripts.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void StartGame()
    {
        SceneManager.LoadScene(LOBBY_LOCATION);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        _gameOverSignal.Raise();
    }
}