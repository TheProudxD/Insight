using Player;
using UnityEngine;

namespace Managers
{
    public class Damager : MonoBehaviour
    {
        private float _attackTimer;
        private DamagerSpecs _damagerSpecs;

        public void Initialize(DamagerSpecs damagerSpecs) => _damagerSpecs = damagerSpecs;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hit(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_attackTimer >= _damagerSpecs.AttackCooldown)
            {
                Hit(collision);
            }
            else
            {
                _attackTimer += Time.deltaTime;
            }
        }

        private void Hit(Component collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                if (damageable is PlayerAttacking && PlayerStateMachine.Current == PlayerState.Stagger)
                    return;

                damageable.Hit(collision.transform.position, _damagerSpecs.KnockTime, _damagerSpecs.Damage);
                Move((Component)damageable);
                _attackTimer = 0;
            }
        }

        private void Move(Component component)
        {
            var entityRB = component.GetComponent<Rigidbody2D>();

            Vector2 difference = entityRB.transform.position - transform.position;
            difference = difference.normalized * _damagerSpecs.Thrust;
            //difference.DOMove(entityRB.transform.position + difference, _knockTime);
            entityRB.AddForce(difference, ForceMode2D.Impulse);
        }
    }
}