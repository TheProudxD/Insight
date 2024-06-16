using Tools;
using UnityEngine;

public class Arrow : PlayerProjectile
{
    public override void Setup(Vector2 velocity, Vector3 direction)
    {
        Rigidbody.velocity = velocity.normalized * Speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InsightUtils.IsItPlayer(other) || other.isTrigger)
            return;
        
        Destroy(gameObject);
        //play anim
    }
}