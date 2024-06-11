using UnityEngine;
using Zenject;

namespace Enemies
{
    public class StaticLog : StaticEnemy
    {        
        [Inject(Id = "static log")] private LogEntitySpecs _specs;

        protected override void Awake()
        {
            base.Awake();
            EnemyHealth.Initialize(_specs.Hp);
            Animator.SetBool(IdleAnimatorKey, true);
        }

        private void OnEnable() => EnemyHealth.Died += OnDied;

        private void OnDisable() => EnemyHealth.Died -= OnDied;

        private void OnDied() => Destroy(gameObject);
        
        protected override void CheckDistance()
        {
            var position = transform.position;
            var distance = Vector3.Distance(Target.position, position);
            if (distance <= _specs.ChaseRadius && distance > _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk)
                {
                    /*var targetDirection = Vector3.MoveTowards(
                        position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);
                        
                    EnemyRigidbody.MovePosition(targetDirection);
                    UpdateMoveAnimation(targetDirection - position);
                    */
                    Animator.SetBool(IdleAnimatorKey, false);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > _specs.ChaseRadius)
            {
                Animator.SetBool(IdleAnimatorKey, true);
                ChangeState(EnemyState.Idle);
            }
        }
    }
}