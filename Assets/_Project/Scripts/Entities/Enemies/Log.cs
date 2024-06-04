using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Enemies
{
    public class Log : Enemy
    {
        [Inject(Id = "static log")] private LogEntitySpecs _specs;
        
        private void FixedUpdate() => CheckDistance();

        private void OnDrawGizmos()
        {
            if (_specs == null)
                return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _specs.ChaseRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _specs.AttackRadius);
        }

        protected virtual void CheckDistance()
        {
            var position = transform.position;
            var distance = Vector3.Distance(Target.position, position);
            if (distance <= _specs.ChaseRadius && distance > _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
                {
                    var targetDirection = Vector3.MoveTowards(
                        position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);
                    EnemyRigidbody.MovePosition(targetDirection);

                    ChangeAnimation(targetDirection - position);
                    SetAnimationBool(AnimationConst.wakeUp, true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > _specs.ChaseRadius)
            {
                SetAnimationBool(AnimationConst.wakeUp, false);
            }
        }

        protected void Move(Vector2 direction) => EnemyRigidbody.MovePosition(direction);

        protected void ChangeAnimation(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                MoveAnimation(direction.x > 0 ? Vector2.right : Vector2.left);
            else
                MoveAnimation(direction.y > 0 ? Vector2.up : Vector2.down);
        }
    }
}