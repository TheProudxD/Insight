using System;
using System.Collections;
using Game.Scripts.Storage;
using Player;
using StorageService;
using UI;
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
        [Inject] private Hud _hud;
        private const string ConditionToNewLevel = "FadeNewLevel";

        private Animator _fadeAnimator;
        private Image _loadingBar;

        private void Awake()
        {
            _fadeAnimator = _hud.FadeAnimator;
            _loadingBar = _hud.LoadingBar;
        }

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
            _loadingBar.fillAmount = 0f;
            
            
            _fadeAnimator.SetTrigger(ConditionToNewLevel);
            yield return new WaitForSecondsRealtime(0.01f);
            _levelManager.StartNextLevel();
            yield return new WaitForSecondsRealtime(0.01f);
            _fadeAnimator.SetTrigger(ConditionToNewLevel);
        }
    }
}