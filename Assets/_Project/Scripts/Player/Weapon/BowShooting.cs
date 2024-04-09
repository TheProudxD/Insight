using System.Collections;
using UnityEngine;

namespace Player
{
    public class BowShooting: Shooting
    {
        [SerializeField] private PlayerProjectile _arrowProjectile;
        protected override float TimeBeforeLastAttackCounter { get; set; }
        
        private readonly float _destroyArrowTime = 3;
        private float _attackDuration;

        public void Initialize(float attackDuration)
        {
            TimeBeforeLastAttackCounter = PlayerEntitySpecs.SwordAttackCooldown;
            _attackDuration = attackDuration;
        }

        public override bool TryShoot(Vector3 position = default, Vector3 direction = default)
        {
            if (!CanAttack())
                return false;

            var playerPos = position;

            ArrowSpawn(playerPos.sqrMagnitude != 0 ? playerPos : direction);

            StartCoroutine(BowAttackCo());
            TimeBeforeLastAttackCounter = 0;
            
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
        
        public IEnumerator BowAttackCo()
        {
            PlayerCurrentState.Current = PlayerState.Attack;
            yield return null;
            yield return new WaitForSeconds(_attackDuration);
            if (PlayerCurrentState.Current != PlayerState.Interact)
                PlayerCurrentState.Current = PlayerState.Walk;
        }
        
        protected override bool CanAttack() => PlayerCurrentState.Current != PlayerState.Attack &&
                                               PlayerCurrentState.Current != PlayerState.Stagger &&
                                               TimeBeforeLastAttackCounter >= PlayerEntitySpecs.BowAttackCooldown;
    }
}