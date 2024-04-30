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
        private readonly int _waitPause = 1000;

        [Inject] private SceneManager _sceneManager;
        [Inject(Id = "fade animator")] private Animator _fadeAnimator;
        private LoadingAnimation _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = GetComponent<LoadingAnimation>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (InsightUtils.IsItPlayer(collision.collider) == false)
                return;
            
            StartTransition();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (InsightUtils.IsItPlayer(collision.collider) == false)
                return;
            
            StopTransition();
        }

        private void StopTransition() => _loadingAnimation.Reset();

        private void StartTransition() => _loadingAnimation.Animate(Transit);

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