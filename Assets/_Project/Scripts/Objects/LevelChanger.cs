using System.Collections;
using Game.Scripts.Storage;
using Player;
using StorageService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Objects
{
    public class LevelChanger : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;
        private const string ConditionToNewLevel = "FadeNewLevel";

        [FormerlySerializedAs("FadeAnimator")] [SerializeField]
        private Animator _fadeAnimator;

        [SerializeField] private Image _loadingBar;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController player))
                StartTransition();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController player))
                StopTransition();
        }

        private void StopTransition()
        {
            StopAllCoroutines();
            _loadingBar.fillAmount = 0f;
        }

        private void StartTransition() => StartCoroutine(LoadingTimer());

        private IEnumerator LoadingTimer()
        {
            while (_loadingBar.fillAmount < 1f)
            {
                _loadingBar.fillAmount += 0.1f;
                yield return new WaitForSecondsRealtime(0.15f);
            }

            _fadeAnimator.SetTrigger(ConditionToNewLevel);
            _loadingBar.fillAmount = 0f;
        }

        public void OnFadeComplete()
        {
            _fadeAnimator.SetTrigger(ConditionToNewLevel);
            _levelManager.StartNextLevel();
        }

        public void FadeGameOver() => _fadeAnimator.SetTrigger("FadeGameOver");
    }
}