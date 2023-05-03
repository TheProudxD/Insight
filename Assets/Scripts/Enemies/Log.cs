using UnityEngine;

public class Log : Enemy
{
    private Transform _target;
    private Transform _homePosition;
    private float _chaseRadius = 4;
    private float _attackRadius = 1.5f;
    
    private void Start()
    {
        CurrentState = EnemyState.Idle;
        _rigidbody = GetComponent<Rigidbody2D>();
        _homePosition = transform;
        _target = FindObjectOfType<PlayerMovement>().transform;
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
            if (CurrentState==EnemyState.Idle || CurrentState==EnemyState.Walk && CurrentState != EnemyState.Idle)
            {
                Vector3 dir = Vector3.MoveTowards(
                    transform.position,
                    _target.position,
                    _moveSpeed * Time.deltaTime);
                _rigidbody.MovePosition(dir);

                ChangeState(EnemyState.Walk);
            }
        }
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
