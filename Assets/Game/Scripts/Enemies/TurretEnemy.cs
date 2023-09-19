using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class TurretEnemy : Log
    {
        [SerializeField] private RockProjectile _projectile;

        private readonly float _fireDelay = 2;
        private float _fireDelayTimer;
        private bool _canFire;

        private void Update()
        {
            _fireDelayTimer -= Time.deltaTime;
            if (_fireDelayTimer <= 0)
            {
                _canFire = true;
                _fireDelayTimer = _fireDelay;
            }
        }

        protected override void CheckDistance()
        {
            var distance = Vector3.Distance(Target.position, transform.position);
            if (distance <= ChaseRadius && distance > AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
                {
                    if (!_canFire) return;
                    var delta = Target.transform.position - transform.position;
                    var currentProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
                    currentProjectile.Launch(delta, BaseAttack);
                    _canFire = false;
                    SetWakeupAnimation(true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > ChaseRadius)
            {
                SetWakeupAnimation(false);
            }
        }
    }
}