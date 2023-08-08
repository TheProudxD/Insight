using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private FloatValue _maxHealth;
    [SerializeField] private Slider _healthBar;
    private float _health;

    private void Start()
    {
        if (_maxHealth is null)
            throw new Exception(nameof(_maxHealth));
        _health = _maxHealth.RuntimeValue;
        _healthBar.maxValue = _health;
        _healthBar.value = _health;
        _healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        _healthBar.gameObject.SetActive(true);
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        // sound + effects
        _healthBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}