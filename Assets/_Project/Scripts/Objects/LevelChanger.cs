using System;
using System.Threading.Tasks;
using Storage;
using Tools;
using UnityEngine;
using Zenject;

namespace Objects
{
    [RequireComponent(typeof(LoadingAnimation))]
    public class LevelChanger : MonoBehaviour
    {
        private readonly int _waitPause = 1500;

        [Inject] private SceneManager _sceneManager;
        [Inject(Id = "fade animator")] private Animator _fadeAnimator;
        
        private LoadingAnimation _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = GetComponent<LoadingAnimation>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            StartTransition();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            StopTransition();
        }

        private void StopTransition() => _loadingAnimation.Reset();

        private void StartTransition() => _loadingAnimation.Animate(Transit, 0.25f);

        private async void Transit()
        {
            _loadingAnimation.Reset();
            _fadeAnimator.SetTrigger("Fade");
            
            await Task.Delay(_waitPause);
            
            _sceneManager.StartNextLevel();
            _fadeAnimator.SetTrigger("Fade");
        }
    }
}