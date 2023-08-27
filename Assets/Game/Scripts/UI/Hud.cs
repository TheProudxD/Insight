using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _changeWeaponButton;
    [SerializeField] private Button _useFirstPotionButton, _useSecondPotionButton, _useThirdPotionButton;

    [SerializeField] private Slider _hpSlider, _manaSlider;
    [SerializeField] private PlayerController _player;

    private void Awake()
    {
        if (_player == null) Debug.LogError(nameof(_player));

        _attackButton.onClick.AddListener(Attack);
        _changeWeaponButton.onClick.AddListener(ChangeWeapon);

        _useFirstPotionButton.onClick.AddListener(UseFirstPotion);
        _useSecondPotionButton.onClick.AddListener(UseSecondPotion);
        _useThirdPotionButton.onClick.AddListener(UseThirdPotion);
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
        _player.TryAttack();
    }

    private void ChangeWeapon()
    {
        print(MethodBase.GetCurrentMethod().Name);
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