using System.Threading.Tasks;
using Storage;
using Tools;
using UI;
using UnityEngine;
using Zenject;

namespace Objects
{
    [RequireComponent(typeof(LoadingAnimation))]
    public class LevelChanger : MonoBehaviour
    {
        private static readonly int Fade = Animator.StringToHash("Fade");

        [Inject] private LevelManager _levelManager;
        [Inject] private Hud _hud;

        private readonly int _waitPause = 1000;
        private Animator _fadeAnimator;
        private LoadingAnimation _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = GetComponent<LoadingAnimation>();
            _fadeAnimator = _hud.FadeAnimator;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
                StartTransition();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
                StopTransition();
        }

        private void StopTransition() => _loadingAnimation.Reset();

        private void StartTransition() => _loadingAnimation.Animate(Transit);

        private async void Transit()
        {
            _loadingAnimation.Reset();
            _fadeAnimator.SetTrigger(Fade);
            await Task.Delay(_waitPause);
            _levelManager.StartNextLevel();
            _fadeAnimator.SetTrigger(Fade);
        }
    }
}