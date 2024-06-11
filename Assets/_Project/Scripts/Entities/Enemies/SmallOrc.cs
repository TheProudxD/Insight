using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class SmallOrc : MovableEnemy
    {
        [Inject(Id = "small orc")] private OrcEntitySpecs _specs;

        private bool _faceRight = true;

        protected override string IdleAnimatorKey => "idle";
        protected override string AttackAnimatorKey => "attack";
        protected override string DeadAnimatorKey => "dead";
        protected override string MoveAnimatorKey => "walk";
        protected string JumpAnimatorKey => "jump";
        protected string ShieldAnimatorKey => "shield_mode";

        private void OnEnable() => EnemyHealth.Died += OnDied;

        private void OnDisable() => EnemyHealth.Died -= OnDied;

        protected override void Awake()
        {
            base.Awake();
            EnemyHealth.Initialize(_specs.Hp);
        }

        private IEnumerator AttackCo()
        {
            Damager.enabled = true;
            Animator.SetBool(AttackAnimatorKey, true);
            ChangeState(EnemyState.Attack);
            yield return null;
            ChangeState(EnemyState.Walk);
            Damager.enabled = false;
        }

        private void TryFlipSprite(Vector2 targetDirection)
        {
            switch (targetDirection.x)
            {
                case > 0 when _faceRight == false:
                    _faceRight = true;
                    Flip();
                    break;
                case < 0 when _faceRight:
                    _faceRight = false;
                    Flip();
                    break;
            }

            return;

            void Flip()
            {
                var theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

        protected override bool IsEnoughToChase(float distanceToTarget) => distanceToTarget <= _specs.ChaseRadius;

        protected override bool IsEnoughToAttack(float distanceToTarget) => distanceToTarget <= _specs.AttackRadius;

        protected override void Move(Vector2 direction)
        {
            EnemyRigidbody.velocity = direction * _specs.MoveSpeed;
            TryFlipSprite(direction);
            ChangeState(EnemyState.Walk);
        }

        protected override void PlayMoveAnimation(Vector2 direction)
        {
            Animator.SetBool(MoveAnimatorKey, true);
            Animator.SetBool(AttackAnimatorKey, false);
            Animator.SetBool(JumpAnimatorKey, false);
        }

        protected override void Attack() => StartCoroutine(AttackCo());

        private void OnDied()
        {
            Animator.SetBool(DeadAnimatorKey, true);
            Destroy(gameObject, _specs.DestroyTimeAfterDying);
            //gameObject.SetActive(false);
        }

        private void PlayShieldMode(bool aux)
        {
            Animator.SetBool(ShieldAnimatorKey, aux);
            Animator.SetBool(MoveAnimatorKey, false);
        }

        private void Jump()
        {
            if (Animator.GetBool(JumpAnimatorKey) == false)
            {
                EnemyRigidbody.AddForce(new Vector2(0f, 200));
                Animator.SetBool(JumpAnimatorKey, true);
            }
        }
    }
}