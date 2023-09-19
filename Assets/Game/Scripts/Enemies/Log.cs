using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class Log : Enemy
    {
        [FormerlySerializedAs("_attackRadius")] [SerializeField]
        protected float AttackRadius = 1.5f;

        [FormerlySerializedAs("_chaseRadius")] [SerializeField]
        protected float ChaseRadius = 4;

        private void FixedUpdate() => CheckDistance();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ChaseRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRadius);
        }

        protected virtual void CheckDistance()
        {
            var distance = Vector3.Distance(Target.position, transform.position);
            if (distance <= ChaseRadius && distance > AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
                {
                    var targetDirection = Vector3.MoveTowards(
                        transform.position,
                        Target.position,
                        MoveSpeed * Time.deltaTime);
                    EnemyRigidbody.MovePosition(targetDirection);

                    ChangeAnimation(targetDirection - transform.position);
                    SetAnimationBool(AnimationConst.wakeUp, true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > ChaseRadius)
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