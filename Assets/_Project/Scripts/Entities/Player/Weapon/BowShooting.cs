using System.Collections;
using UnityEngine;

namespace Player
{
    public class BowShooting : Shooting
    {
        [SerializeField] private PlayerProjectile _arrowProjectile;
        protected override float TimeBeforeLastAttackCounter { get; set; }

        private float _destroyArrowTime;
        private float _attackDuration;

        public void Initialize()
        {
            TimeBeforeLastAttackCounter = PlayerEntitySpecs.BowAttackDuration;
            _attackDuration = PlayerEntitySpecs.BowAttackDuration;
            _destroyArrowTime = PlayerEntitySpecs.DestroyArrowTime;
        }

        public override bool TryShoot(Vector3 position = default, Vector3 direction = default)
        {
            if (!CanAttack())
                return false;

            var playerPos = position;
            ArrowSpawn(playerPos.sqrMagnitude != 0 ? playerPos : direction);
            StartCoroutine(BowAttackCo());
            CharacterAudioPlayer.PlayAttack(AttackType.Bow);
            return true;
        }

        private void ArrowSpawn(Vector3 position)
        {
            var arrow = Instantiate(_arrowProjectile, transform.position, Quaternion.identity);

            var directionZ = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            var direction = new Vector3(0, 0, directionZ);
            arrow.Setup(position, direction);

            Destroy(arrow.gameObject, _destroyArrowTime);
        }

        private IEnumerator BowAttackCo()
        {
            TimeBeforeLastAttackCounter = 0;
            PlayerStateMachine.Current = PlayerState.Attack;
            yield return null;
            yield return new WaitForSeconds(_attackDuration);
            if (PlayerStateMachine.Current != PlayerState.Interact)
                PlayerStateMachine.Current = PlayerState.Idle;
        }

        protected override bool CanAttack() => PlayerStateMachine.Current != PlayerState.Attack &&
                                               PlayerStateMachine.Current != PlayerState.Stagger &&
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.BowAttackCooldown;
    }
}