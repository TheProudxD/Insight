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
        private PlayerController _playerController;

        private void Awake()
        {
            var gm = GetComponentInChildren<GameManager>();
            var hud = GetComponentInChildren<Hud>();
            _playerController = GetComponentInChildren<PlayerController>();

            _levelManager.LevelChanged += ChangePlayerPosition;
            DontDestroyOnLoad(gameObject);
        }

        private void ChangePlayerPosition(Levels level)
        {
            _playerController.transform.position = _levelSpawnData.SpawnPosition[level];
        }
    }
}