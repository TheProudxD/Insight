using System;
using System.Reflection;
using Player;
using StorageService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(CameraInjector))]
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _changeWeaponButton;
        [SerializeField] private Button _useFirstPotionButton, _useSecondPotionButton, _useThirdPotionButton;
        
        [SerializeField] private FloatValue _hpValue;
        [SerializeField] private FloatValue _manaValue;

        [SerializeField] private Slider _hpSlider, _manaSlider;
        [SerializeField] private PlayerController _player;
        [SerializeField] private TextMeshProUGUI _playerNickname;

        [Inject] private DataManager _dataManager;

        private void Awake()
        {
            if (_player == null)
            {
                Debug.LogError(nameof(_player));
                return;
            }

            _attackButton.onClick.AddListener(Attack);
            _changeWeaponButton.onClick.AddListener(ChangeWeapon);

            _useFirstPotionButton.onClick.AddListener(UseFirstPotion);
            _useSecondPotionButton.onClick.AddListener(UseSecondPotion);
            _useThirdPotionButton.onClick.AddListener(UseThirdPotion);

            _playerNickname.text = _dataManager.GetName();

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

        private void DecreaseBar(Slider slider, float amount)
        {
            if (slider is null)
                throw new NullReferenceException(nameof(slider));
            slider.value -= amount;
        }

        private void Attack()
        {
            _player.TryFirstAttack();
        }

        private void ChangeWeapon()
        {
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
    }
}