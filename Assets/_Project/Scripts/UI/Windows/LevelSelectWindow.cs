using Managers;
using Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelSelectWindow : CommonWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private Button _homeLevelButton;
        [SerializeField] private Button _wayToDungeonLevelButton;
        [SerializeField] private Button _dungeon1Button;
        [SerializeField] private Button _dungeon2Button;
        [SerializeField] private Button _dungeonBossButton;

        [Inject] private SceneManager _sceneManager;
        [Inject] private WindowManager _windowManager;

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => _windowManager.CloseLevelSelectWindow());
            _menuButton.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.Menu);
            });
            _homeLevelButton.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.LovelyHome);
            });
            _wayToDungeonLevelButton.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.WayToDungeon);
            });
            _dungeon1Button.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.Dungeon1Floor);
            });
            _dungeon2Button.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.Dungeon2Floor);
            });
            _dungeonBossButton.onClick.AddListener(() =>
            {
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scenes.DungeonBoss);
            });
        }
    }
}