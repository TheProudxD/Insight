using System;
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
        [Inject] private LevelManager _levelManager;
        [Inject] private Hud _hud;
        private PlayerAttacking _playerAttacking;

        private void Awake()
        {
            var gm = GetComponentInChildren<GameStateManager>();
            _playerAttacking = GetComponentInChildren<PlayerAttacking>();

            _levelManager.LevelChanged += ChangePlayerPosition;
            _hud.DisableView();
            _playerAttacking.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        private void ChangePlayerPosition(Levels level)
        {
            if (level > Levels.Menu)
            {
                _hud.EnableView();
                _playerAttacking.gameObject.SetActive(true);
            }

            _playerAttacking.transform.position = _levelSpawnData.SpawnPosition[level];
        }
    }
}