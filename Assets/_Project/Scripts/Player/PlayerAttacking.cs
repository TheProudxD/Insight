using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerMana))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAttacking : MonoBehaviour
    {
        public event Action<AttackType> Attacked;

        [SerializeField] private Signal _playerHitSignal;
        [SerializeField] private PlayerProjectile _playerProjectile;

        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private PlayerMana _playerMana;
        private float _timeBeforeLastAttackCounter;
        private float _attackCooldown = 0.4f;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerMana = GetComponent<PlayerMana>();
            _timeBeforeLastAttackCounter = _attackCooldown;
        }

        private void Update()
        {
            if (PlayerCurrentState.Current == PlayerState.Interact)
                return;

            if (_timeBeforeLastAttackCounter < _attackCooldown)
            {
                _timeBeforeLastAttackCounter += Time.deltaTime;
            }
        }

        private void ArrowSpawn(Vector3 position)
        {
            var arrow = Instantiate(_playerProjectile, transform.position, Quaternion.identity);

            var directionZ = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            var direction = new Vector3(0, 0, directionZ);
            arrow.Setup(position, direction);

            Destroy(arrow, 10);
        }

        private bool IsCanAttack() => PlayerCurrentState.Current != PlayerState.Attack &&
                                      PlayerCurrentState.Current != PlayerState.Stagger &&
                                      _timeBeforeLastAttackCounter >= _attackCooldown;

        public void SwordAttack()
        {
            if (!IsCanAttack()) 
                return;

            _playerMana.Decrease(1);
            StartCoroutine(_playerAnimation.SwordAttackCo());
            _timeBeforeLastAttackCounter = 0;
            Attacked?.Invoke(AttackType.Sword);
        }

        public void BowAttack()
        {
            if (!IsCanAttack()) return;

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
            
            Attacked?.Invoke(AttackType.Bow);
        }

        public IEnumerator KnockCoroutine(float knockTime, float damage)
        {
            _playerHitSignal.Raise();
            _playerHealth.Decrease(damage);

            if (_playerHealth.Value <= 0)
                yield break;

            PlayerCurrentState.Current = PlayerState.Idle;
            yield return new WaitForSeconds(knockTime);
            PlayerCurrentState.Current = PlayerState.Idle;
            _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
        }
    }

    public enum AttackType
    {
        Sword,
        Bow
    }
}