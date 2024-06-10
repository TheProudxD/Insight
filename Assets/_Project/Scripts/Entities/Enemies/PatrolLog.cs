using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class PatrolLog : MovableEnemy
    {
        [Inject(Id = "dynamic log")] private LogEntitySpecs _specs;

        protected override string IdleAnimatorKey => "wakeUp";
        protected override string MoveAnimatorKey { get; }
        protected override string AttackAnimatorKey { get; }
        protected override string DeadAnimatorKey { get; }

        protected override void Attack() => StartCoroutine(AttackCo());

        public IEnumerator AttackCo()
        {
            Damager.enabled = true;
            Animator.SetBool(AttackAnimatorKey, true);
            ChangeState(EnemyState.Attack);
            yield return new WaitForSeconds(1f);
            ChangeState(EnemyState.Walk);
            Damager.enabled = false;
        }

        protected override void Move(Vector2 direction)
        {
            EnemyRigidbody.MovePosition(direction * _specs.MoveSpeed);
            ChangeState(EnemyState.Walk);
        }

        protected override void PlayMoveAnimation(Vector2 direction)
        {
            direction -= (Vector2)transform.position;

            Vector2 dir;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                dir = direction.x > 0 ? Vector2.right : Vector2.left;
            else
                dir = direction.y > 0 ? Vector2.up : Vector2.down;

            Animator.SetFloat("moveX", dir.x);
            Animator.SetFloat("moveY", dir.y);
        }

        protected override bool IsEnoughToChase(float distanceToTarget) => distanceToTarget <= _specs.ChaseRadius;

        protected override bool IsEnoughToAttack(float distanceToTarget) => distanceToTarget <= _specs.AttackRadius;
    }
}