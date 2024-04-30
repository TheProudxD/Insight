using Objects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(ImageLoadingAnimation))]
    public class PlayerAbilitySystemView : MonoBehaviour
    {
        [Inject] private PlayerAbilitySystem _playerAbilitySystem;

        [SerializeField] private Button _firstAbilityButton;
        [SerializeField] private Button _secondAbilityButton;
        [SerializeField] private Button _thirdAbilityButton;

        private ImageLoadingAnimation _loadingAnimation;
        private Image _firstAbilityImage;
        private Image _secondAbilityImage;
        private Image _thirdAbilityImage;

        private void Awake()
        {
            _loadingAnimation = GetComponent<ImageLoadingAnimation>();

            _firstAbilityButton.onClick.AddListener(UseFirstAbility);
            _secondAbilityButton.onClick.AddListener(UseSecondAbility);
            _thirdAbilityButton.onClick.AddListener(UseThirdAbility);

            _firstAbilityImage = _firstAbilityButton.GetComponent<Image>();
            _secondAbilityImage = _secondAbilityButton.GetComponent<Image>();
            _thirdAbilityImage = _thirdAbilityButton.GetComponent<Image>();
        }

        private void UseFirstAbility()
        {
            var duration = _playerAbilitySystem.UseDash();
            PlayLoading(_firstAbilityButton, _firstAbilityImage, duration);
        }

        private void UseSecondAbility()
        {
            var duration = _playerAbilitySystem.UseMultiProjectile();
            PlayLoading(_secondAbilityButton, _secondAbilityImage, duration);
        }

        private void UseThirdAbility()
        {
            var duration = _playerAbilitySystem.UseFireCircle();
            PlayLoading(_thirdAbilityButton, _thirdAbilityImage, duration);
        }
        
        private void PlayLoading(Button button, Image image, float duration)
        {
            if (duration <= 0)
                return;
            
            button.interactable = false;
            _loadingAnimation.Animate(image, 1 / duration, () => button.interactable = true);
        }
    }
}