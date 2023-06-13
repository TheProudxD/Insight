using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const string LOBBY_LOCATION = "Lobby";
    private const string LEVEL_DATA = "Level";

    [NonSerialized] public bool IsDoorOpened;
    [NonSerialized] public int GameLevel;

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

            if (PlayerPrefs.GetInt(LEVEL_DATA) <=2)
                PlayerPrefs.SetInt(LEVEL_DATA, 2);
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(LOBBY_LOCATION);
    }

    public void SaveData()
    {
        if (SceneManager.GetActiveScene().name == LOBBY_LOCATION)
            GameLevel = PlayerPrefs.GetInt(LEVEL_DATA);
        else
            GameLevel++;

        int level = PlayerPrefs.GetInt(LEVEL_DATA);
        if (GameLevel > level)
            PlayerPrefs.SetInt(LEVEL_DATA, GameLevel);

        PlayerPrefs.Save();
    }
}