using System;
using Managers;
using Objects;
using Player;
using StorageService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _changeWeaponButton;
        [SerializeField] private Button _useFirstPotionButton, _useSecondPotionButton, _useThirdPotionButton;
        [SerializeField] private Button _settingsButton, _inventoryButton;
        
        [SerializeField] private Slider _hpSlider, _manaSlider;
        [SerializeField] private TextMeshProUGUI _playerNickname;
        [SerializeField] private Animator _fadeAnimator;
        
        [Inject] private PlayerAttacking _player;
        [Inject] private PlayerAbilitySystem _playerAbilitySystem;
        [Inject] private WindowManager _windowManager;
        [Inject] private DataManager _dataManager;
        [Inject] private PlayerEntitySpecs _playerEntitySpecs;

        public Animator FadeAnimator => _fadeAnimator;
        private AbilityLoadingAnimation _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = GetComponent<AbilityLoadingAnimation>();
            
            _attackButton.onClick.AddListener(FirstAttack);
            _changeWeaponButton.onClick.AddListener(SecondAttack);

            _useFirstPotionButton.onClick.AddListener(UseFirstAbility);
            _useSecondPotionButton.onClick.AddListener(UseSecondAbility);
            _useThirdPotionButton.onClick.AddListener(UseThirdAbility);

            _inventoryButton.onClick.AddListener(OpenInventory);
            _settingsButton.onClick.AddListener(OpenPause);

            InitializeHpBar();
            InitializeManaBar();
            
            _dataManager.DataLoaded+= SetNicknameAfterLoading;
        }

        private void SetNicknameAfterLoading(PlayerData pd) => SetPlayerNickname(pd.Name);

        private void InitializeHpBar()
        {
            _hpSlider.maxValue = _playerEntitySpecs.HpAmount;
            _hpSlider.minValue = 0;
            _hpSlider.value = _playerEntitySpecs.HpAmount;
        }

        private void InitializeManaBar()
        {
            _manaSlider.maxValue = _playerEntitySpecs.ManaAmount;
            _manaSlider.minValue = 0;
            _manaSlider.value = _playerEntitySpecs.ManaAmount;
        }

        private void DecreaseBar(Slider slider, float amount)
        {
            if (slider is null)
                throw new NullReferenceException(nameof(slider));
            slider.value -= amount;
        }

        public void ChangeHealthBarAmount(float amount) => DecreaseBar(_hpSlider, amount);

        public void ChangeManaBarAmount(float amount) => DecreaseBar(_manaSlider, amount);

        private void SetPlayerNickname(string nick) => _playerNickname.text = nick;

        private void FirstAttack() => _player.SwordAttack();

        private void SecondAttack() => _player.BowAttack();

        private void UseFirstAbility()
        {
            _loadingAnimation.Animate(_useFirstPotionButton.GetComponent<Image>(),0.2f);
            _playerAbilitySystem.UseDash();
        }

        private void UseSecondAbility() => _playerAbilitySystem.UseMultiProjectile();

        private void UseThirdAbility() => _playerAbilitySystem.UseFireCircle();

        private void OpenInventory() => _windowManager.ShowInventoryWindow();

        private void OpenPause() => _windowManager.ShowPauseWindow();

        private void SwitchView(bool state)
        {
            for (int i = 0; i < transform.childCount; i++) 
                transform.GetChild(i).gameObject.SetActive(state);
        }
        
        public void DisableView() => SwitchView(false);
        
        public void EnableView() => SwitchView(true);
    }
}