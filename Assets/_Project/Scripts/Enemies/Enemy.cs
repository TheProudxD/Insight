using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyName"), SerializeField]
        protected string EnemyName;

        [FormerlySerializedAs("_baseAttack"), SerializeField]
        protected int BaseAttack;

        [FormerlySerializedAs("_moveSpeed"), SerializeField]
        protected float MoveSpeed;

        protected EnemyState CurrentState { get; private set; }
        protected Rigidbody2D EnemyRigidbody;
        protected Transform Target;
        private Animator Animator;

        protected void Awake()
        {
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            Target = FindObjectOfType<PlayerAttacking>().transform;
            Animator = GetComponent<Animator>();
            Animator.SetBool(AnimationConst.wakeUp.ToString(), true);
            CurrentState = EnemyState.Idle;
        }

        protected void ChangeState(EnemyState newState)
        {
            if (CurrentState != newState)
                CurrentState = newState;
        }

        protected void MoveAnimation(Vector2 direction) =>
            SetAnimationFloat(AnimationConst.moveX, AnimationConst.moveY, direction);

        protected void SetAnimationBool(AnimationConst animation, bool enable) =>
            Animator.SetBool(animation.ToString(), enable);

        private void SetAnimationFloat(AnimationConst animation1, AnimationConst animation2, Vector2 direction)
        {
            Animator.SetFloat(animation1.ToString(), direction.x);
            Animator.SetFloat(animation2.ToString(), direction.y);
        }

        public IEnumerator KnockCoroutine(float knockTime)
        {
            yield return new WaitForSeconds(knockTime);
            EnemyRigidbody.velocity = Vector2.zero;
            CurrentState = EnemyState.Idle;
            EnemyRigidbody.velocity = Vector2.zero;
        }
    }
}