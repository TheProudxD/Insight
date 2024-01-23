using System;
using System.Reflection;
using Managers;
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

        [SerializeField] private FloatValue _hpValue;
        [SerializeField] private FloatValue _manaValue;

        [SerializeField] private Slider _hpSlider, _manaSlider;
        [SerializeField] private TextMeshProUGUI _playerNickname;
        [SerializeField] private Animator _fadeAnimator;
        [SerializeField] private Image _loadingBar;
        [SerializeField] private Joystick _joystick;

        [Inject] private WindowManager _windowManager;

        public Animator FadeAnimator => _fadeAnimator;
        public Image LoadingBar => _loadingBar;
        public Joystick Joystick => _joystick;

        private PlayerController _player;

        private void Awake()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerController>();
                //Debug.LogError(nameof(_player));
                // return;
            }

            _attackButton.onClick.AddListener(Attack);
            _changeWeaponButton.onClick.AddListener(ChangeWeapon);

            _useFirstPotionButton.onClick.AddListener(UseFirstPotion);
            _useSecondPotionButton.onClick.AddListener(UseSecondPotion);
            _useThirdPotionButton.onClick.AddListener(UseThirdPotion);

            _inventoryButton.onClick.AddListener(OpenInventory);
            _settingsButton.onClick.AddListener(OpenSettings);
            
            InitializeHpBar();
            InitializeManaBar();
        }

        private void InitializeHpBar()
        {
            _hpSlider.maxValue = _hpValue.InitialValue;
            _hpSlider.minValue = 0;
            _hpSlider.value = _hpValue.RuntimeValue;
        }

        private void InitializeManaBar()
        {
            _manaSlider.maxValue = _manaValue.InitialValue;
            _manaSlider.minValue = 0;
            _manaSlider.value = _manaValue.RuntimeValue;
        }

        public void ChangeHealthBarAmount(float amount)
        {
            DecreaseBar(_hpSlider, amount);
        }

        public void ChangeManaBarAmount(float amount)
        {
            DecreaseBar(_manaSlider, amount);
        }

        public void SetPlayerNickname(string nick) => _playerNickname.text = nick;

        private void DecreaseBar(Slider slider, float amount)
        {
            if (slider is null)
                throw new NullReferenceException(nameof(slider));
            slider.value -= amount;
        }

        private void Attack()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerController>();
            }
            _player.TryFirstAttack();
        }

        private void ChangeWeapon()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerController>();
            }
            _player.TrySecondAttack();
        }

        private void TakeFirstWeapon()
        {
            print(MethodBase.GetCurrentMethod().Name);
        }

        private void TakeSecondWeapon()
        {
            print(MethodBase.GetCurrentMethod().Name);
        }

        private void UseFirstPotion()
        {
            print(MethodBase.GetCurrentMethod().Name);
        }

        private void UseSecondPotion()
        {
            print(MethodBase.GetCurrentMethod().Name);
        }

        private void UseThirdPotion()
        {
            print(MethodBase.GetCurrentMethod().Name);
        }

        private void OpenInventory()
        {
        }

        private void OpenSettings()
        {
            _windowManager.OpenPauseWindow();
        }
    }
}