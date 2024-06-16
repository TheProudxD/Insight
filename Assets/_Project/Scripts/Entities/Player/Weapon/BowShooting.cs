using System.Collections;
using UnityEngine;
using Zenject;

namespace Player
{
    public class BowShooting : Shooting
    {
        [Inject(Id = "player")] private DamagerSpecs _damagerSpecs;
        [SerializeField] private PlayerProjectile _arrowProjectile;
        protected override float TimeBeforeLastAttackCounter { get; set; }

        private float _destroyArrowTime;
        private float _attackDuration;

        public void Initialize()
        {
            TimeBeforeLastAttackCounter = PlayerEntitySpecs.BowAttackDuration;
            _attackDuration = PlayerEntitySpecs.BowAttackDuration;
            _destroyArrowTime = PlayerEntitySpecs.DestroyArrowTime;
            _arrowProjectile.Initialize(_damagerSpecs);
        }

        public override bool TryShoot(Vector3 position = default, Vector3 direction = default)
        {
            if (CanAttack() == false)
                return false;

            ArrowSpawn(direction);
            StartCoroutine(BowAttackCo());
            CharacterAudioPlayer.PlayAttack(AttackType.Bow);
            return true;
        }

        private void ArrowSpawn(Vector3 position)
        {
            var arrow = Instantiate(_arrowProjectile, transform.position, Quaternion.identity);
            arrow.Initialize(_damagerSpecs);
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
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.BowAttackCooldown &&
                                               PlayerMana.Enough(PlayerEntitySpecs.BowShootingPrice);
    }
}