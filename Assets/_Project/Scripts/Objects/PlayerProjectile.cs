using Managers;
using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected float Speed;
    protected Damager Damager;
    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Damager = GetComponent<Damager>();
    }

    public void Initialize(DamagerSpecs damagerSpecs) => Damager.Initialize(damagerSpecs);

    public abstract void Setup(Vector2 velocity, Vector3 direction);
}