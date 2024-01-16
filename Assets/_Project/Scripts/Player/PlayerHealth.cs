using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private FloatValue _currentHealth;
        [SerializeField] private Signal _healthSignal;

        public float CurrentHealth
        {
            get => _currentHealth.RuntimeValue;
            private set => _currentHealth.RuntimeValue = value;
        }

        private int MaxHealth => 9;

        private void Start()
        {
            _healthSignal.Raise(MaxHealth);
        }

        public bool TryIncreaseHealth(float amount)
        {
            _healthSignal.Raise(-amount);
            if (Math.Abs(CurrentHealth - MaxHealth) < 0.01f)
                return false;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            return true;
        }

        public void DecreaseHealth(float damage)
        {
            _healthSignal.Raise();
            _healthSignal.Raise(damage);
            CurrentHealth -= damage;

            print(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                print("death started");
                StartCoroutine(GameManager.Instance.GameOver());
            }
        }
    }
}