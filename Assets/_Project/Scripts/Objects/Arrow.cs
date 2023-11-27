using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : PlayerProjectile
{
    public override void Setup(Vector2 velocity, Vector3 direction)
    {
        Rigidbody.velocity = velocity.normalized * Speed;
        transform.rotation = Quaternion.Euler(direction);
    }
}

public abstract class PlayerProjectile: MonoBehaviour
{
    [SerializeField] protected float Speed;
    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void Setup(Vector2 velocity, Vector3 direction);
}
