using Objects;
using UnityEngine;

public class BoundedNpc : Interactable
{
    [SerializeField] private Collider2D _bounds;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _minMoveTime;
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxWaitTime;

    private Vector3 _directionVector;
    private bool _isMoving;
    private float _moveTimeSeconds;
    private float _waitTimeSeconds;

    private void Awake()
    {
        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
        ChangeDirection();
    }

    public void Update()
    {
        if (_isMoving)
        {
            _moveTimeSeconds -= Time.deltaTime;
            if (_moveTimeSeconds <= 0)
            {
                _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                _isMoving = false;
            }

            if (!PlayerInRange)
            {
                Move();
            }
        }
        else
        {
            _waitTimeSeconds -= Time.deltaTime;
            if (_waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                _isMoving = true;
                _waitTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
            }
        }
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = _directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == _directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = transform.position + _directionVector * (_speed * Time.deltaTime);
        if (_bounds.bounds.Contains(temp))
        {
            _rigidbody.MovePosition(temp);
        }
        else
        { 
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        _directionVector = Random.Range(0, 4) switch
        {
            0 => Vector3.right,
            1 => Vector3.up,
            2 => Vector3.left,
            3 => Vector3.down,
            _ => _directionVector
        };
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("MoveX", _directionVector.x);
        _animator.SetFloat("MoveY", _directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }
}