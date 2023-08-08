using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class PatrolLog : Log
{
    [SerializeField] private List<Transform> _waypoints;
    private Transform CurrentPoint => _waypoints[_currentPointIndex];
    private int _currentPointIndex;
    private readonly float _roundingDistance = 0.1f;
    protected new void Awake()
    {
        base.Awake();
        _waypoints.ForEach(x => x.parent = null);
    }
    protected override void CheckDistance()
    {
        var distance = Vector3.Distance(Target.position, transform.position);
        if (distance <= _chaseRadius && distance > _attackRadius)
        {
            if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
            {
                var targetDirection = Vector3.MoveTowards(
                    transform.position,
                    Target.position,
                    MoveSpeed * Time.deltaTime);
                Move(targetDirection);
                ChangeAnimation(targetDirection - transform.position);
                SetWakeupAnimation(true);
                ChangeState(EnemyState.Walk);
            }
        }
        else if (distance > _chaseRadius)
        {
            if (Vector3.Distance(transform.position, CurrentPoint.position) > _roundingDistance)
            {
                var direction = Vector3.MoveTowards(transform.position, CurrentPoint.position,
                    MoveSpeed * Time.deltaTime);
                ChangeAnimation(direction - transform.position);
                Move(direction);
            }
            else
            {
                ChangeWaypointIndex();
            }
        }
    }

    private void ChangeWaypointIndex()
    {
        if (_currentPointIndex >= _waypoints.Count - 1)
        {
            _currentPointIndex = 0;
        }
        else
        {
            _currentPointIndex++;
        }
    }
}