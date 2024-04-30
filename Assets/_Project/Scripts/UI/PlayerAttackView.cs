using Objects;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(ImageLoadingAnimation))]
    public class PlayerAttackView : MonoBehaviour
    {
        [Inject] private PlayerAttacking _player;
        [Inject] private PlayerEntitySpecs _playerEntitySpecs;

        [SerializeField] private Button _firstAttackButton;
        [SerializeField] private Button _secondAttackButton;

        private ImageLoadingAnimation _loadingAnimation;
        private Image _firstAttackImage;
        private Image _secondAttackImage;

        private void Awake()
        {
            _loadingAnimation = GetComponent<ImageLoadingAnimation>();

            _firstAttackButton.onClick.AddListener(FirstAttack);
            _secondAttackButton.onClick.AddListener(SecondAttack);

            _firstAttackImage = _firstAttackButton.GetComponent<Image>();
            _secondAttackImage = _secondAttackButton.GetComponent<Image>();
        }

        private void FirstAttack()
        {
            _player.SwordAttack();
            PlayLoading(_firstAttackButton, _firstAttackImage, _playerEntitySpecs.SwordAttackCooldown);
        }

        private void SecondAttack()
        {
            _player.BowAttack();
            PlayLoading(_secondAttackButton, _secondAttackImage, _playerEntitySpecs.BowAttackCooldown);
        }

        private void PlayLoading(Button button, Image image, float duration)
        {
            button.interactable = false;
            _loadingAnimation.Animate(image, 1 / duration, () => button.interactable = true);
        }
    }
}