using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class Log : Enemy
    {
        [Inject(Id = "static log")] private LogEntitySpecs _specs;

        protected override string IdleAnimatorKey => "wakeUp";
        protected string MoveXAnimatorKey => "moveX";
        protected string MoveYAnimatorKey => "moveY";
        protected override string AttackAnimatorKey { get; }
        protected override string DeadAnimatorKey { get; }

        protected override void Awake()
        {
            base.Awake();
            Animator.SetBool(IdleAnimatorKey, true);
        }

        protected override void Attack() => StartCoroutine(AttackCo());

        private IEnumerator AttackCo()
        {
            Damager.enabled = true;
            Animator.SetBool(AttackAnimatorKey, true);
            ChangeState(EnemyState.Attack);
            yield return null;
            ChangeState(EnemyState.Walk);
            Damager.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (_specs == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _specs.ChaseRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _specs.AttackRadius);
        }

        private void SetAnimationFloat(string animationKey1, string animationKey2, Vector2 direction)
        {
            Animator.SetFloat(animationKey1, direction.x);
            Animator.SetFloat(animationKey2, direction.y);
        }

        protected void Move(Vector2 direction) => EnemyRigidbody.MovePosition(direction);

        protected void UpdateMoveAnimation(Vector2 direction)
        {
            Vector2 dir;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                dir = direction.x > 0 ? Vector2.right : Vector2.left;
            else
                dir = direction.y > 0 ? Vector2.up : Vector2.down;

            SetAnimationFloat(MoveXAnimatorKey, MoveYAnimatorKey, dir);
        }

        protected void SetAnimationBool(string animation, bool enable) =>
            Animator.SetBool(animation, enable);

        protected override void CheckDistance()
        {
            var position = transform.position;
            var distance = Vector3.Distance(Target.position, position);
            if (distance <= _specs.ChaseRadius && distance > _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk)
                {
                    var targetDirection = Vector3.MoveTowards(
                        position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);
                    EnemyRigidbody.MovePosition(targetDirection);

                    UpdateMoveAnimation(targetDirection - position);
                    SetAnimationBool(IdleAnimatorKey, false);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance > _specs.ChaseRadius)
            {
                SetAnimationBool(IdleAnimatorKey, true);
                ChangeState(EnemyState.Idle);
            }
        }
    }
}