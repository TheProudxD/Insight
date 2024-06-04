using Player;
using UnityEngine;

namespace Managers
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private float _thrust = 4f;
        [SerializeField] private float _knockTime = 0.4f;
        [SerializeField] private float _damage = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IKnockbackable knockbackable))
            {
                if (knockbackable is PlayerAttacking && PlayerStateMachine.Current == PlayerState.Stagger) 
                    return;
                
                knockbackable.Hit(collision.transform.position, _knockTime, _damage);
                MoveEntity((Component)knockbackable);
            }
        }

        private void MoveEntity(Component knockbackable)
        {
            var entityRB = knockbackable.GetComponent<Rigidbody2D>();

            Vector2 difference = entityRB.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            //difference.DOMove(entityRB.transform.position + difference, _knockTime);
            entityRB.AddForce(difference, ForceMode2D.Impulse);
        }
    }
}