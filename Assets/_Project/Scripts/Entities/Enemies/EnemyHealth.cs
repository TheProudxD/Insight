using System;
using Objects.Powerups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        private const string DEATH_ANIMATOR_KEY = "death";

        private static readonly int DeathHashID = Animator.StringToHash(DEATH_ANIMATOR_KEY);

        [Inject(Id = "enemyDeathEffect")] private Animator _deathAnimator;
        [Inject] private PowerupFactory _powerupFactory;

        private LootTable _lootTable;
        private Slider _healthBar;
        private float _health;
        private bool _initialized;

        public event Action Died;

        public void Initialize(float health, LootTable lootTable)
        {
            _health = health;
            _lootTable = lootTable;
            
            _healthBar = GetComponentInChildren<Slider>();
            _healthBar.maxValue = _health;
            _healthBar.value = _health;
            _healthBar.gameObject.SetActive(false);
            _initialized = true;
        }

        public void TakeDamage(float damage)
        {
            CheckInit();
            _health -= damage;
            _healthBar.value = _health;
            _healthBar.gameObject.SetActive(true);
            if (_health <= 0)
                Die();
        }

        private void MakeLoot()
        {
            CheckInit();

            if (_lootTable == null)
                throw new NullReferenceException(nameof(_lootTable));

            var powerup = _lootTable.LootPowerup();
            _powerupFactory.Create(powerup, transform.position);
        }

        private void Die()
        {
            // + sound
            Died?.Invoke();
            MakeLoot();
            _deathAnimator.gameObject.transform.position = transform.position;
            _deathAnimator.SetTrigger(DeathHashID);
            _healthBar.gameObject.SetActive(false);
        }

        private void CheckInit()
        {
            if (_initialized == false)
                throw new NullReferenceException(nameof(EnemyHealth) + "is not initialized");
        }
    }
}