using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class PatrolLog : MovableEnemy
    {
        [Inject(Id = "dynamic log")] private LogEntitySpecs _specs;
        [Inject(Id = "dynamic log")] private DamagerSpecs _damagerSpecs;
        [Inject(Id = "100% heart")] private LootTable _lootTable;

        protected override string IdleAnimatorKey { get; }
        protected override string MoveAnimatorKey => "wakeUp";
        protected override string AttackAnimatorKey { get; }
        protected override string DeadAnimatorKey { get; }

        protected override void Awake()
        {
            base.Awake();

            EnemyHealth.Initialize(_specs.Hp, _lootTable);
            Damager.Initialize(_damagerSpecs);
        }

        private void OnEnable() => EnemyHealth.Died += OnDied;

        private void OnDisable() => EnemyHealth.Died -= OnDied;

        private IEnumerator AttackCo()
        {
            Damager.enabled = true;
            Animator.SetBool(MoveAnimatorKey, true);
            ChangeState(EnemyState.Attack);
            yield return new WaitForSeconds(1f);
            ChangeState(EnemyState.Walk);
            Damager.enabled = false;
        }

        private void SetAnimationFloat(string animationKey1, string animationKey2, Vector2 direction)
        {
            Animator.SetFloat(animationKey1, direction.x);
            Animator.SetFloat(animationKey2, direction.y);
        }

        private void OnDied()
        {
            //Animator.SetBool(DeadAnimatorKey, true);
            Destroy(gameObject, _specs.DestroyTimeAfterDying);
            //gameObject.SetActive(false);
        }

        protected override void Attack() => StartCoroutine(AttackCo());

        protected override void Move(Vector2 direction)
        {
            Animator.SetBool(MoveAnimatorKey, true);
            EnemyRigidbody.velocity = direction * _specs.MoveSpeed;
            ChangeState(EnemyState.Walk);
        }

        protected override void PlayMoveAnimation(Vector2 direction)
        {
            Vector2 dir;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                dir = direction.x > 0 ? Vector2.right : Vector2.left;
            else
                dir = direction.y > 0 ? Vector2.up : Vector2.down;

            SetAnimationFloat("moveX", "moveY", dir);
        }

        protected override bool IsEnoughToChase(float distanceToTarget) => distanceToTarget <= _specs.ChaseRadius;

        protected override bool IsEnoughToAttack(float distanceToTarget) => distanceToTarget <= _specs.AttackRadius;
    }
}