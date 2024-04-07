using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float SPEED = 50f;

    private Rigidbody2D _playerRigidbody2D;
    private Vector3 _moveDir;
    [SerializeField] private int _health;

    private void Awake()
    {
        _playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        _moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        bool isIdle = _moveDir.x == 0 && _moveDir.y == 0;
        if (isIdle)
        {
            //playerBase.PlayIdleAnim();
        }
        else
        {
            //playerBase.PlayMoveAnim(moveDir);
            //transform.position += moveDir * SPEED * Time.deltaTime;
            _playerRigidbody2D.MovePosition(transform.position + _moveDir * SPEED * Time.fixedDeltaTime);
        }
    }

    public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance) =>
        transform.position += knockbackDir * knockbackDistance;

    public Vector3 GetPosition() => transform.position;

    public bool IsDead() => _health <= 0;
}