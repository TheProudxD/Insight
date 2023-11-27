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
        [SerializeField] private PlayerProjectile _playerProjectile;

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

        private bool IsCanAttack() => CurrentState != PlayerState.Attack &&
                                      CurrentState != PlayerState.Stagger &&
                                      _timeBeforeLastAttackCounter >= _attackCooldown;
        public bool TryFirstAttack()
        {
            if (!IsCanAttack()) return false;
            
            StartCoroutine(_playerAnimation.SwordAttackCo());
            _timeBeforeLastAttackCounter = 0;
            return true;
        }
        
        public bool TrySecondAttack()
        {
            if (!IsCanAttack()) 
                return false;

            var playerPos = _playerMovement.PlayerMovementVector;

            if (playerPos.sqrMagnitude != 0)
            {
                ArrowSpawn(playerPos);
            }
            else
            {
                ArrowSpawn(_playerMovement.PlayerDirectionVector);
            }
           
            StartCoroutine(_playerAnimation.BowAttackCo());
            _timeBeforeLastAttackCounter = 0;
            return true;
        }

        private void ArrowSpawn(Vector3 position)
        {
            var arrow = Instantiate(_playerProjectile, transform.position, Quaternion.identity);

            var directionZ = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            var direction = new Vector3(0, 0, directionZ);
            arrow.Setup(position, direction);

            Destroy(arrow, 10);
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