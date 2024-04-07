using System;
using UnityEngine;

public class PlayerPlatformer : MonoBehaviour
{
    public event EventHandler OnDead;

    private const float MOVE_SPEED = 40f;
    [SerializeField] private LayerMask _platformsLayerMask;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private bool _waitForStart;
    private bool _isDead;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _boxCollider = transform.GetComponent<BoxCollider2D>();
        _isDead = false;
    }

    private void Start()
    {
        //PlayMoveAnim(Vector2.right);
    }

    private void Update()
    {
        if (_isDead)
            return;

        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                float jumpVelocity = 100f;
                _rigidbody.velocity = Vector2.up * jumpVelocity;
            }
        }

        HandleMovement();
        HandleAnimations();

        if (_rigidbody.velocity.y < -300f)
        {
            // Falling way too fast, dead
            Die();
        }
    }

    private void HandleAnimations()
    {
        if (IsGrounded())
        {
            if (_rigidbody.velocity.x == 0)
            {
                // PlayIdleAnim();
            }
            else
            {
                //PlayMoveAnim(new Vector2(GetVelocity().x, 0f));
            }
        }
        else
        {
            //PlayJumpAnim(rigidbody2d.velocity);
        }
    }

    private bool IsGrounded()
    {
        var bounds = _boxCollider.bounds;
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(bounds.center, bounds.size, 0f,
            Vector2.down, 1f, _platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        _rigidbody.velocity = new Vector2(moveX * MOVE_SPEED, _rigidbody.velocity.y);
    }

    public Vector3 GetPosition() => transform.position;

    public Vector3 GetVelocity() => _rigidbody.velocity;

    private void Die()
    {
        _isDead = true;
        _rigidbody.velocity = Vector3.zero;
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}