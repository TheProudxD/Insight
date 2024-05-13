using Managers;
using Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelSelectWindow : CommonWindow
    {        
        [Inject] private SceneManager _sceneManager;
        [Inject] private WindowManager _windowManager;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _menuButton;

        [field: SerializeField] public Sprite Star { get; private set; }
        [field: SerializeField] public Sprite PlaceholderStar { get; private set; }
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(() => _windowManager.CloseLevelSelectWindow());
            _menuButton.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.Menu);
            });
        }
    }
}