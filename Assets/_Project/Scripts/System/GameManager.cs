using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
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
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public IEnumerator GameOver()
        {
            yield return new WaitForSeconds(1f);
            _gameOverSignal.Raise();
        }
    }
}