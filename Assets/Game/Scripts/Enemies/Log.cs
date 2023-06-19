using UnityEngine;

public class Log : Enemy
{
    private Animator _logAnimator;
    private Transform _target;
    private Transform _homePosition;
    private float _chaseRadius = 4;
    private float _attackRadius = 1.5f;

    private void Awake()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _logAnimator = GetComponent<Animator>();
        _target = FindObjectOfType<PlayerController>().transform;
        _homePosition = transform;
    }

    private new void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        var distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= _chaseRadius && distance > _attackRadius)
        {
            if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
            {
                Vector3 targetDirecton = Vector3.MoveTowards(
                    transform.position,
                    _target.position,
                    _moveSpeed * Time.deltaTime);
                _enemyRigidbody.MovePosition(targetDirecton);

                ChangeAnimation(targetDirecton - transform.position);
                _logAnimator.SetBool(WAKEUP_STATE, true);

                ChangeState(EnemyState.Walk);
            }
        }
        else if (distance > _chaseRadius)
        {
            _logAnimator.SetBool(WAKEUP_STATE, false);
        }
    }

    private void SetAnimationFloat(Vector2 direction)
    {
        _logAnimator.SetFloat(XMOVE_STATE, direction.x);
        _logAnimator.SetFloat(YMOVE_STATE, direction.y);
    }
    private void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            SetAnimationFloat(direction.x > 0 ? Vector2.right: Vector2.left);
        else
            SetAnimationFloat(direction.y > 0 ? Vector2.up : Vector2.down);
    }
    private void ChangeState(EnemyState newState)
    {
        if (CurrentState!= newState)
        {
            CurrentState = newState;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
