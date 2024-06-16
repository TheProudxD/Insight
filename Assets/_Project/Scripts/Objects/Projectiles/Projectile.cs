using Managers;
using UnityEngine;
using Zenject;

namespace Objects
{
    [RequireComponent(typeof(Damager))]
    public class Projectile : MonoBehaviour
    {
        private float _lifetime;
        private float _moveSpeed;
        private float _lifetimeTimer;
        private Vector2 _directionToMove;
        private Rigidbody2D _rigidbody;
        private Damager _damager;

        public void Initialize(DamagerSpecs damagerSpecs, float lifetime, float moveSpeed)
        {
            _damager.Initialize(damagerSpecs);
            _lifetime = lifetime;
            _moveSpeed = moveSpeed;
            _lifetimeTimer = _lifetime;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _damager = GetComponent<Damager>();
        }

        private void Update()
        {
            _lifetimeTimer -= Time.deltaTime;
            if (_lifetimeTimer <= 0)
                Destroy();
        }

        public void Launch(Vector2 direction) => _rigidbody.velocity = direction * _moveSpeed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger)
                return;

            Destroy();
        }

        private void Destroy()
        {
            GetComponent<Animator>().SetTrigger("Brake");
            _rigidbody.velocity = Vector2.zero;
            Destroy(gameObject, 0.75F);
        }
    }
}