using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private float _moveSpeed;

    private Vector2 _directionToMove;
    private float _lifetimeTimer;
    private Rigidbody2D _rigidbody;
    private float _damage;

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

    public void Launch(Vector2 direction, float damage)
    {
        _rigidbody.velocity = direction * _moveSpeed;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //animation or particles...
        Destroy(gameObject);
    }
}