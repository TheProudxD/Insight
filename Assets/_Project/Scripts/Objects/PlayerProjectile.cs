using UnityEngine;

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