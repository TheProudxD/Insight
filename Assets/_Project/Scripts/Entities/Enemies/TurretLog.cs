using UnityEngine;
using Zenject;

namespace Enemies
{
    public class TurretLog : StaticEnemy
    {
        [SerializeField] private RockProjectile _projectile;
        [Inject(Id = "turret log")] private LogEntitySpecs _specs;

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
            if (distance <= _specs.ChaseRadius && distance > _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
                {
                    if (!_canFire)
                        return;

                    var position = transform.position;
                    var delta = Target.transform.position - position;
                    var currentProjectile = Instantiate(_projectile, position, Quaternion.identity);
                    currentProjectile.Launch(delta);
                    _canFire = false;
                    Animator.SetBool(IdleAnimatorKey, true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > _specs.ChaseRadius)
            {
                Animator.SetBool(IdleAnimatorKey, false);
                ChangeState(EnemyState.Idle);
            }
        }
    }
}