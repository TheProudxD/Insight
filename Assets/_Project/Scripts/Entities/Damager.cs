using Player;
using UnityEngine;

namespace Managers
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private float _thrust = 4f;
        [SerializeField] private float _knockTime = 0.4f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _attackCooldown = 1.5f;
        
        private float _attackTimer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hit(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_attackTimer >= _attackCooldown)
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

                damageable.Hit(collision.transform.position, _knockTime, _damage);
                Move((Component)damageable);
                _attackTimer = 0;
            }
        }

        private void Move(Component component)
        {
            var entityRB = component.GetComponent<Rigidbody2D>();

            Vector2 difference = entityRB.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            //difference.DOMove(entityRB.transform.position + difference, _knockTime);
            entityRB.AddForce(difference, ForceMode2D.Impulse);
        }
    }
}