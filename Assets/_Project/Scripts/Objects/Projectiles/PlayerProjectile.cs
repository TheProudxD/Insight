using Managers;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public abstract class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected float Speed;
    protected Damager Damager;
    protected Rigidbody2D Rigidbody;

    public void Initialize(DamagerSpecs damagerSpecs)
    {
        Damager = GetComponent<Damager>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Damager.Initialize(damagerSpecs);
    }

    public abstract void Setup(Vector2 velocity, Vector3 direction);
}