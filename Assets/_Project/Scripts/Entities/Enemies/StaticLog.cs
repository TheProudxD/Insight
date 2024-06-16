using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class StaticLog : StaticEnemy
    {
        [Inject(Id = "static log")] private LogEntitySpecs _specs;
        [Inject(Id = "static log")] private DamagerSpecs _damagerSpecs;
        [Inject(Id = "50% 50%")] private LootTable _lootTable;

        protected override void Awake()
        {
            base.Awake();
            EnemyHealth.Initialize(_specs.Hp, _lootTable);
            Damager.Initialize(_damagerSpecs);
            Animator.SetBool(IdleAnimatorKey, false);
            ChangeState(EnemyState.Idle);
        }

        private void OnEnable() => EnemyHealth.Died += OnDied;

        private void OnDisable() => EnemyHealth.Died -= OnDied;

        private void OnDied() => Destroy(gameObject);

        private IEnumerator AttackCo()
        {
            Damager.enabled = true;
            ChangeState(EnemyState.Attack);
            yield return null;
            ChangeState(EnemyState.Idle);
            Damager.enabled = false;
        }

        protected override void CheckDistance()
        {
            var position = transform.position;
            var distance = Vector3.Distance(Target.position, position);
            if (distance <= _specs.AttackRadius)
            {
                /*var targetDirection = Vector3.MoveTowards(
                        position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);

                    EnemyRigidbody.MovePosition(targetDirection);
                    UpdateMoveAnimation(targetDirection - position);
                    */

                Attack();
            }
        }

        protected override void Attack() => StartCoroutine(AttackCo());
    }
}