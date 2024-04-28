using Enemies;
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
            if (collision.TryGetComponent(out Enemy enemy))
            {
                MoveEntity(enemy);
                StartCoroutine(enemy.KnockCoroutine(_knockTime, _damage));
            }
            else if (collision.TryGetComponent(out PlayerAttacking player) &&
                     PlayerStateMachine.Current != PlayerState.Stagger)
            {
                MoveEntity(player);
                StartCoroutine(player.KnockCoroutine(_knockTime, _damage));
            }
        }

        private void MoveEntity(Component entity)
        {
            var entityRB = entity.GetComponent<Rigidbody2D>();

            Vector2 difference = entityRB.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            //difference.DOMove(entityRB.transform.position + difference, _knockTime);
            entityRB.AddForce(difference, ForceMode2D.Impulse);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }
    }
}