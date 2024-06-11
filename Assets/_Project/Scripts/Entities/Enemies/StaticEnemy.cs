using System.Collections;

namespace Enemies
{
    public abstract class StaticEnemy : Enemy
    {
        protected override string IdleAnimatorKey => "wakeUp";
        protected override string AttackAnimatorKey { get; }
        protected override string DeadAnimatorKey { get; }

        private IEnumerator AttackCo()
        {
            Damager.enabled = true;
            Animator.SetBool(AttackAnimatorKey, true);
            ChangeState(EnemyState.Attack);
            yield return null;
            ChangeState(EnemyState.Walk);
            Damager.enabled = false;
        }
        
        protected override void Attack() => StartCoroutine(AttackCo());
    }
}