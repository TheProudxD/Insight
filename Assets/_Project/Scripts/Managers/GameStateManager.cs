using System.Collections;
using StorageService;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;

        [Inject] private DataManager _dataManager;
        [SerializeField] private Signal _gameOverSignal;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public IEnumerator GameOver()
        {
            print("death started");
            yield return new WaitForSeconds(1f);
            _gameOverSignal.Raise();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Time.timeScale = hasFocus ? 1 : 0;
        }

        private void OnApplicationQuit()
        {
            _dataManager.SaveQuitTime();
        }
    }
}