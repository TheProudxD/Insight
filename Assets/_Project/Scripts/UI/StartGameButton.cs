using Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartGameButton : MonoBehaviour
{
    [Inject] private SceneManager _sceneManager;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(LoadLobby);
    }

    private void LoadLobby()
    {
        _sceneManager.StartNextLevel();
    }
}