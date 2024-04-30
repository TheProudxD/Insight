using System.Collections;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerMana))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAttacking : MonoBehaviour
    {
        [Inject] protected PlayerEntitySpecs PlayerEntitySpecs;
        
        [SerializeField] private Signal _playerHitSignal;
        [SerializeField] private HitAnimation _hitAnimation;
        [SerializeField] private BowShooting _bowShooting;
        [SerializeField] private SwordShooting _swordShooting;

        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private PlayerMana _playerMana;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerMana = GetComponent<PlayerMana>();
            
            _bowShooting.Initialize();
            _swordShooting.Initialize(_playerAnimation);
        }

        public void SwordAttack()
        {
            if (_swordShooting.TryShoot())
            {
                _playerMana.Decrease(PlayerEntitySpecs.SwordShootingPrice);
            }
        }

        public void BowAttack()
        {
            if (_bowShooting.TryShoot(_playerMovement.PlayerMovementVector, _playerMovement.GetFaceDirection()))
            {
                _playerMana.Decrease(PlayerEntitySpecs.BowShootingPrice);
            }
        }

        public IEnumerator KnockCoroutine(float knockTime, float damage)
        {
            _playerHitSignal.Raise();
            _playerHitSignal.Raise(damage);
            _playerHealth.Decrease(damage);

            if (_playerHealth.Amount <= 0)
                yield break;

            _hitAnimation.Play();
            yield return new WaitForSeconds(knockTime);
            PlayerStateMachine.Current = PlayerState.Idle;
            _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
        }
    }
}