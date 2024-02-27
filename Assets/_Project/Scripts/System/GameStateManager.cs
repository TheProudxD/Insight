using System.Collections;
using UnityEngine;

namespace Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        
        [SerializeField] private Signal _gameOverSignal;

        private void Awake()
        {
            if (Instance != null)
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
    }
}