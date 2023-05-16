using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform WindowCanvas;
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

            if (PlayerPrefs.GetInt("Level")<=2)
                PlayerPrefs.SetInt("Level", 2);
        }
    }
}