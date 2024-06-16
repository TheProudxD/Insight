using Objects;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class TurretLog : StaticEnemy
    {
        [Inject(Id = "turret log")] private LogEntitySpecs _specs;
        [Inject(Id = "turret log")] private DamagerSpecs _damagerSpecs;
        [Inject(Id = "75% coin")] private LootTable _lootTable;

        [SerializeField] private RockProjectile _projectile;

        private readonly float _fireDelay = 2;
        private float _fireDelayTimer;
        private bool _canFire;

        protected override void Awake()
        {
            base.Awake();
            _fireDelayTimer = _fireDelay;
            Damager.Initialize(_damagerSpecs);
            EnemyHealth.Initialize(_specs.Hp, _lootTable);
            ChangeState(EnemyState.Idle);
        }

        private void Update()
        {
            _fireDelayTimer -= Time.deltaTime;
            if (_fireDelayTimer <= 0 && _canFire == false)
            {
                _canFire = true;
                _fireDelayTimer = _fireDelay;
            }
        }

        protected override void CheckDistance()
        {
            var distance = Vector3.Distance(Target.position, transform.position);
            if (distance <= _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk)
                {
                    if (!_canFire)
                        return;

                    Animator.SetBool(IdleAnimatorKey, true);
                    var position = transform.position;
                    var delta = Target.transform.position - position;
                    var currentProjectile = Instantiate(_projectile, position, Quaternion.identity);
                    currentProjectile.Initialize(_damagerSpecs, 1, 1);
                    currentProjectile.Launch(delta);
                    _canFire = false;
                    ChangeState(EnemyState.Attack);
                }
            }
            else
            {
                Animator.SetBool(IdleAnimatorKey, false);
                ChangeState(EnemyState.Idle);
            }
        }
    }
}