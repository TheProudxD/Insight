using Player;
using Storage;
using UI;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PersistenceDataChanger : MonoBehaviour
    {
        [SerializeField] private LevelSpawnPosition _levelSpawnData;
        [Inject] private SceneManager _sceneManager;
        [Inject] private Hud _hud;
        private PlayerAttacking _playerAttacking;

        private void Awake()
        {
            _playerAttacking = GetComponentInChildren<PlayerAttacking>();

            _sceneManager.LevelChanged += ChangePlayerPosition;
            _hud.DisableView();
            _playerAttacking.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        private void ChangePlayerPosition(Scene scene)
        {
            if (scene > Scene.Menu)
            {
                _hud.EnableView();
                _playerAttacking.gameObject.SetActive(true);
            }

            _playerAttacking.transform.position = _levelSpawnData.SpawnPosition[scene];
        }
    }
}