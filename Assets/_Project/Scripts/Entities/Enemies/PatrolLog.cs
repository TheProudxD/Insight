using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Enemies
{
    public class PatrolLog : Log
    {
        [Inject(Id = "dynamic log")] private LogEntitySpecs _specs;
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
            if (distance <= _specs.ChaseRadius && distance > _specs.AttackRadius)
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk and not EnemyState.Idle)
                {
                    var targetDirection = Vector3.MoveTowards(
                        transform.position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);
                   
                    Move(targetDirection);
                    
                    ChangeAnimation(targetDirection - transform.position);
                    SetAnimationBool(AnimationConst.wakeUp, true);
                    ChangeState(EnemyState.Walk);
                }
            }
            else if (distance <= _specs.AttackRadius)
            {
                if (CurrentState == EnemyState.Walk
                    && CurrentState != EnemyState.Stagger)
                {
                    var targetDirection = Vector3.MoveTowards(
                        transform.position,
                        Target.position,
                        _specs.MoveSpeed * Time.deltaTime);
                    Move(targetDirection);
                    //StartCoroutine(AttackCo());
                }
            }
            else if (distance > _specs.ChaseRadius)
            {
                if (Vector3.Distance(transform.position, CurrentPoint.position) > _roundingDistance)
                {
                    var direction = Vector3.MoveTowards(transform.position, CurrentPoint.position,
                        _specs.MoveSpeed * Time.deltaTime);
                    ChangeAnimation(direction - transform.position);
                    Move(direction);
                }
                else
                {
                    ChangeWaypointIndex();
                }
            }
        }
        
        public IEnumerator AttackCo()
        {
            ChangeState(EnemyState.Attack);
            yield return new WaitForSeconds(1f);
            ChangeState(EnemyState.Walk);
        }

        private void ChangeWaypointIndex()
        {
            if (_currentPointIndex >= _waypoints.Count - 1)
                _currentPointIndex = 0;
            else
                _currentPointIndex++;
        }
    }
}