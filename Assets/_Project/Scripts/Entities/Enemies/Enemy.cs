using System.Collections;
using Managers;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(HitParticleAnimation))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        protected EnemyState CurrentState { get; private set; }
        protected Rigidbody2D EnemyRigidbody;
        protected Transform Target;
        protected EnemyHealth EnemyHealth;
        protected Animator Animator;
        protected Damager Damager;
        protected abstract string IdleAnimatorKey { get; }
        protected abstract string AttackAnimatorKey { get; }
        protected abstract string DeadAnimatorKey { get; }

        private HitParticleAnimation _hitParticleAnimation;
        
        protected virtual void Awake()
        {
            _hitParticleAnimation = GetComponent<HitParticleAnimation>();
            EnemyHealth = GetComponent<EnemyHealth>();
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            Target = FindObjectOfType<PlayerAttacking>().transform;
            Animator = GetComponent<Animator>();
            Damager = GetComponent<Damager>();
            CurrentState = EnemyState.Idle;
        }

        protected virtual void FixedUpdate() => CheckDistance();
        
        private IEnumerator KnockCoroutine(float knockTime, float damage)
        {
            EnemyHealth.TakeDamage(damage);
            CurrentState = EnemyState.Stagger;
            EnemyRigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(knockTime);
            CurrentState = EnemyState.Idle;
            EnemyRigidbody.velocity = Vector2.zero;
        }

        private void HitParticle(Vector3 position) => _hitParticleAnimation.Hit(position);

        protected void ChangeState(EnemyState newState) => CurrentState = newState;

        protected abstract void Attack();
        
        protected abstract void CheckDistance();
        
        public void Hit(Vector3 position, float knockTime, float damage)
        {
            if (gameObject.activeSelf == false)
                return;

            HitParticle(position);
            StartCoroutine(KnockCoroutine(knockTime, damage));
        }
    }
}