using System.Collections;
using UnityEngine;

namespace Player
{
    public class SwordShooting : Shooting
    {
        protected override float TimeBeforeLastAttackCounter { get; set; }
        
        private PlayerAnimation _playerAnimation;
        private float _attackDuration;

        public void Initialize(PlayerAnimation playerAnimation)
        {
            TimeBeforeLastAttackCounter = PlayerEntitySpecs.SwordAttackCooldown;
            _attackDuration = PlayerEntitySpecs.SwordAttackDuration;
            _playerAnimation = playerAnimation;
        }

        public override bool TryShoot(Vector3 position = default, Vector3 direction = default)
        {
            if (!CanAttack())
                return false;

            StartCoroutine(SwordAttackCo());

            return true;
        }

        private IEnumerator SwordAttackCo()
        {
            TimeBeforeLastAttackCounter = 0;
            _playerAnimation.SetAttackingAnimation(true);
            PlayerStateMachine.Current = PlayerState.Attack;
            yield return null;
            _playerAnimation.SetAttackingAnimation(false);
            yield return new WaitForSeconds(_attackDuration);
            if (PlayerStateMachine.Current != PlayerState.Interact)
                PlayerStateMachine.Current = PlayerState.Idle;
        }

        protected override bool CanAttack() => PlayerStateMachine.Current != PlayerState.Attack &&
                                               PlayerStateMachine.Current != PlayerState.Stagger &&
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.SwordAttackCooldown;
    }
}