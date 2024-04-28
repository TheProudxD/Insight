using System.Collections;
using UnityEngine;

namespace Player
{
    public class SwordShooting : Shooting
    {
        protected override float TimeBeforeLastAttackCounter { get; set; }
        private PlayerAnimation _playerAnimation;
        private float _attackDuration; 

        public void Initialize(PlayerAnimation playerAnimation, float attackDuration)
        {
            TimeBeforeLastAttackCounter = PlayerEntitySpecs.SwordAttackCooldown;
            _playerAnimation = playerAnimation;
            _attackDuration = attackDuration;
        }

        public override bool TryShoot(Vector3 position = default, Vector3 direction = default)
        {
            if (!CanAttack())
                return false;

            StartCoroutine(SwordAttackCo());
            TimeBeforeLastAttackCounter = 0;

            return true;
        }

        public IEnumerator SwordAttackCo()
        {
            _playerAnimation.SetAttackingAnimation(true);
            PlayerStateMachine.Current = PlayerState.Attack;
            yield return null;
            _playerAnimation.SetAttackingAnimation(false);
            yield return new WaitForSeconds(_attackDuration);
            if (PlayerStateMachine.Current != PlayerState.Interact)
                PlayerStateMachine.Current = PlayerState.Walk;
        }

        protected override bool CanAttack() => PlayerStateMachine.Current != PlayerState.Attack &&
                                               PlayerStateMachine.Current != PlayerState.Stagger &&
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.SwordAttackCooldown;
    }
}