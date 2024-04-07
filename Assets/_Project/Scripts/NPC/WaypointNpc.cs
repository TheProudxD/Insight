using System.Collections.Generic;
using Objects;
using Tools;
using UnityEngine;

public class WaypointNpc : Interactable
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _minMoveTime;
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxWaitTime;

    private bool _isMoving;
    private float _moveTimeSeconds;
    private float _waitTimeSeconds;
    private int _indexWp;

    private void Awake()
    {
        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);

        _waypoints.ForEach(x => x.parent = null);
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
            if (_waitTimeSeconds > 0)
                return;

            _isMoving = true;
            _waitTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        }
    }

    private void Move()
    {
        var wp = _waypoints[_indexWp].position;

        if (Vector3.Distance(transform.position, wp) > 0.1f)
        {
            var direction = Vector3.MoveTowards(transform.position, wp, _speed * Time.deltaTime);
            _rigidbody.MovePosition(direction);
        }
        else
        {
            ChangeWaypointIndex();
            UpdateAnimation();
        }
    }

    private void ChangeWaypointIndex()
    {
        if (_indexWp >= _waypoints.Count - 1)
            _indexWp = 0;
        else
            _indexWp++;
    }

    private void UpdateAnimation() => _animator.SetFloat("MoveX", _indexWp % 2);

    private void RotateNPCToPlayer(float playerPosition)
    {
        var NPCPos = transform.position.x;
        var deltaPos = playerPosition - NPCPos;

        if (deltaPos < 0)
        {
            transform.Rotate(Vector2.up, 180f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (InsightUtils.IsItPlayer(other) == false)
            return;
        
        _animator.SetBool("Idle", true);
        RotateNPCToPlayer(other.transform.position.x);
        Context.Raise();
        PlayerInRange = true;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (InsightUtils.IsItPlayer(other) == false)
            return;
        
        _animator.SetBool("Idle", false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        UpdateAnimation();
        Context.Raise();
        PlayerInRange = false;
    }
}