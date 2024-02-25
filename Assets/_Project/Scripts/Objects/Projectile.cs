using UnityEngine;

namespace Objects
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        [SerializeField] private float _moveSpeed;

        private Vector2 _directionToMove;
        private float _lifetimeTimer;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _lifetimeTimer = _lifetime;
        }

        private void Update()
        {
            _lifetimeTimer -= Time.deltaTime;
            if (_lifetimeTimer <= 0)
                Destroy(gameObject);
        }

        public void Launch(Vector2 direction) => _rigidbody.velocity = direction * _moveSpeed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            GetComponent<Animator>().SetTrigger("Brake");
            _rigidbody.velocity = Vector2.zero;
            Destroy(gameObject, 0.75F);
        }
    }
}