using System;
using System.Reflection;
using Assets._Project.Scripts.Storage.Static;
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

        [SerializeField] private Slider _hpSlider, _manaSlider;
        [SerializeField] private PlayerController _player;
        [SerializeField] private TextMeshProUGUI _playerNickname;
        [Inject] private Camera _uiCamera;
        [Inject] private DataManager _dataManager;

        private void Awake()
        {
            if (_player == null)
            {
                Debug.LogError(nameof(_player));
                return;
            }

            GetComponent<Canvas>().worldCamera = _uiCamera;

            _attackButton.onClick.AddListener(Attack);
            _changeWeaponButton.onClick.AddListener(ChangeWeapon);

            _useFirstPotionButton.onClick.AddListener(UseFirstPotion);
            _useSecondPotionButton.onClick.AddListener(UseSecondPotion);
            _useThirdPotionButton.onClick.AddListener(UseThirdPotion);

            _playerNickname.text = _dataManager.GetName();
        }

        public void ChangeHealthBarAmount(float amount)
        {
            DecreaseBar(_hpSlider, amount);
        }

        private void ChangeManaBarAmount(float amount)
        {
            DecreaseBar(_manaSlider, amount);
        }

        private void DecreaseBar(Slider slider, float amount)
        {
            if (slider is null) throw new NullReferenceException(nameof(slider));
            if (slider.value > 0)
            {
                slider.GetComponentsInChildren<Image>()[1].enabled = true;
                slider.value -= amount;
            }
            else
            {
                slider.GetComponentsInChildren<Image>()[1].enabled = false;
            }
        }

        private void IncreaseBar(Slider slider)
        {
            if (slider is null) return;

            if (!slider.GetComponentsInChildren<Image>()[1].enabled)
                slider.GetComponentsInChildren<Image>()[1].enabled = true;
            else
                slider.value += 10;
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