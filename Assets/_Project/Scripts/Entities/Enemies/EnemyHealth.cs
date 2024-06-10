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

        [Inject(Id = "static log")] private LogEntitySpecs _specs;
        [Inject(Id = "enemyDeathEffect")] private Animator _deathAnimator;
        [Inject] private PowerupFactory _powerupFactory;

        [SerializeField] private Slider _healthBar;
        [SerializeField] private LootTable _lootTable;

        private float _health;

        public event Action Died;

        private void Start()
        {
            _health = _specs.Hp;
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

        private void MakeLoot()
        {
            if (_lootTable == null)
                return;

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
    }
}