using Game.Scripts.Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartGameButton : MonoBehaviour
{
    [Inject] private LevelManager _levelManager;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(LoadLobby);
    }

    private void LoadLobby()
    {
        _levelManager.StartNextLevel();
    }
}