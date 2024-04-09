using System.Collections;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class Enemy : MonoBehaviour
    {
        protected EnemyState CurrentState { get; private set; }
        protected Rigidbody2D EnemyRigidbody;
        protected Transform Target;
        protected EnemyHealth EnemyHealth;

        private Animator _animator;

        protected void Awake()
        {
            EnemyHealth = GetComponent<EnemyHealth>();
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            Target = FindObjectOfType<PlayerAttacking>().transform;
            _animator = GetComponent<Animator>();
            _animator.SetBool(AnimationConst.wakeUp.ToString(), true);
            CurrentState = EnemyState.Idle;
        }

        protected void ChangeState(EnemyState newState) => CurrentState = newState;

        protected void MoveAnimation(Vector2 direction) =>
            SetAnimationFloat(AnimationConst.moveX, AnimationConst.moveY, direction);

        protected void SetAnimationBool(AnimationConst animation, bool enable) =>
            _animator.SetBool(animation.ToString(), enable);

        private void SetAnimationFloat(AnimationConst animation1, AnimationConst animation2, Vector2 direction)
        {
            _animator.SetFloat(animation1.ToString(), direction.x);
            _animator.SetFloat(animation2.ToString(), direction.y);
        }

        public IEnumerator KnockCoroutine(float knockTime, float damage)
        {
            EnemyHealth.TakeDamage(damage);
            CurrentState = EnemyState.Stagger;
            EnemyRigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(knockTime);
            CurrentState = EnemyState.Idle;
            EnemyRigidbody.velocity = Vector2.zero;
        }
    }
}