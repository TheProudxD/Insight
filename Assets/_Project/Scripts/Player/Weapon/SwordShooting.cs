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
            PlayerCurrentState.Current = PlayerState.Attack;
            yield return null;
            _playerAnimation.SetAttackingAnimation(false);
            yield return new WaitForSeconds(_attackDuration);
            if (PlayerCurrentState.Current != PlayerState.Interact)
                PlayerCurrentState.Current = PlayerState.Walk;
        }

        protected override bool CanAttack() => PlayerCurrentState.Current != PlayerState.Attack &&
                                               PlayerCurrentState.Current != PlayerState.Stagger &&
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.SwordAttackCooldown;
    }
}