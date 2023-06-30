using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string LOBBY_LOCATION = "Lobby";
    private const string LEVEL_DATA = "Level";
    public static GameManager Instance;

    [SerializeField] private Signal _gameOverSignal;
    [NonSerialized] public int GameLevel;

    [NonSerialized] public bool IsDoorOpened;

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

            if (PlayerPrefs.GetInt(LEVEL_DATA) <= 2)
                PlayerPrefs.SetInt(LEVEL_DATA, 2);
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

    public void SaveData()
    {
        if (SceneManager.GetActiveScene().name == LOBBY_LOCATION)
        {
            GameLevel = PlayerPrefs.GetInt(LEVEL_DATA);
        }
        else
        {
            GameLevel++;
        }

        var level = PlayerPrefs.GetInt(LEVEL_DATA);
        if (GameLevel > level)
            PlayerPrefs.SetInt(LEVEL_DATA, GameLevel);

        PlayerPrefs.Save();
    }
}