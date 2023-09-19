using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerState CurrentState;

        [SerializeField] private FloatValue _currentHealth;
        [SerializeField] private Signal _healthSignal;
        [SerializeField] private Signal _playerHitSignal;
        [SerializeField] private Slider _healthBar;

        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private float _timeBeforeLastAttackCounter;
        private float MaxHealth => 9;
        private float _attackCooldown = 0.4f;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _timeBeforeLastAttackCounter = _attackCooldown;
            _healthBar.maxValue = MaxHealth;
            _healthBar.value = _currentHealth.RuntimeValue;
            CurrentState = PlayerState.Walk;
        }

        private void Update()
        {
            if (CurrentState == PlayerState.Interact)
                return;

            if (_timeBeforeLastAttackCounter < _attackCooldown)
            {
                _timeBeforeLastAttackCounter += Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (CurrentState != PlayerState.Attack &&
                CurrentState != PlayerState.Stagger &&
                _timeBeforeLastAttackCounter >= _attackCooldown)
            {
                StartCoroutine(_playerAnimation.AttackCo());
                _timeBeforeLastAttackCounter = 0;
                return true;
            }

            return false;
        }

        private void FixedUpdate()
        {
            if (CurrentState is PlayerState.Walk or PlayerState.Idle)
            {
                _playerAnimation.UpdateAnimation(_playerMovement.PlayerMovementVector);
                _playerMovement.MoveCharacter(transform.position);
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth.RuntimeValue -= damage;
            print(_currentHealth.RuntimeValue);
            if (_currentHealth.RuntimeValue <= 0)
                StartCoroutine(GameManager.Instance.GameOver());
        }

        public bool TryIncreaseHealth(float amount)
        {
            _healthSignal.Raise(-amount);
            if (_currentHealth.RuntimeValue == MaxHealth) return false;
            _currentHealth.RuntimeValue += amount;
            _currentHealth.RuntimeValue = Mathf.Clamp(_currentHealth.RuntimeValue, 0, MaxHealth);
            print(_currentHealth.RuntimeValue);
            return true;
        }

        public IEnumerator KnockCoroutine(float knockTime, float damage)
        {
            _healthSignal.Raise(damage);
            _playerHitSignal.Raise();
            TakeDamage(damage);

            if (_currentHealth.RuntimeValue > 0)
            {
                _healthSignal.Raise();

                CurrentState = PlayerState.Idle;
                yield return new WaitForSeconds(knockTime);
                CurrentState = PlayerState.Idle;
                _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
            }
        }
    }
}