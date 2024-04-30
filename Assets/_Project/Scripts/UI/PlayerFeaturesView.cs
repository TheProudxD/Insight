using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerFeaturesView : MonoBehaviour
    {
        [Inject] private PlayerEntitySpecs _playerEntitySpecs;

        [SerializeField] private Slider _hpSlider, _manaSlider;

        private void Awake()
        {
            InitializeHpBar();
            InitializeManaBar();
        }

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
    }
}