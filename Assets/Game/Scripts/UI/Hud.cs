using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider, _manaSlider;
    [SerializeField] private PlayerAnimation _playerAnimation;

    public void DecreaseHPBar(float amount)
    {
        DecreaseBar(_hpSlider, amount);
    }

    public void DecreaseManaBar(float amount)
    {
        DecreaseBar(_manaSlider, amount);
    }

    private void DecreaseBar(Slider slider, float amount)
    {
        if (slider is null) return;
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
    
    public void Attack()
    {
        StartCoroutine(_playerAnimation.AttackCo());
    }
    
    public void TakeFirstWeapon()
    {
        print(MethodBase.GetCurrentMethod().Name);
    }

    public void TakeSecondWeapon()
    {
        print(MethodBase.GetCurrentMethod().Name);
    }

    public void UseFirstPotion()
    {
        print(MethodBase.GetCurrentMethod().Name);
    }

    public void UseSecondPotion()
    {
        print(MethodBase.GetCurrentMethod().Name);
    }
    public void UseThirdPotion()
    {
        print(MethodBase.GetCurrentMethod().Name);
    }
}
