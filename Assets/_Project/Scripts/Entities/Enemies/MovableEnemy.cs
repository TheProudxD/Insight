using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public abstract class MovableEnemy : Enemy
    {
        [SerializeField] protected List<Transform> Waypoints;

        private readonly float _roundingDistance = 1f;

        protected Transform CurrentPoint => Waypoints[CurrentPointIndex];
        protected int CurrentPointIndex;
        protected abstract string MoveAnimatorKey { get; }

        protected override void Awake()
        {
            base.Awake();
            Waypoints.ForEach(x => x.parent = null);
        }

        protected override void CheckDistance()
        {
            var distanceToTarget = GetDistanceToTarget();
            if (IsEnoughToChase(distanceToTarget))
            {
                Chase(distanceToTarget);
            }
            else
            {
                WalkWaypoints();
            }
        }

        private void WalkWaypoints()
        {
            if (GetDistanceToCurrentPoint() > _roundingDistance)
            {
                var direction = GetDirectionToCurrentPoint();
                Move(direction);
                PlayMoveAnimation(direction);
            }
            else
            {
                ChangeWaypointIndex();
            }
        }

        private void Chase(float distanceToTarget)
        {
            if (IsEnoughToAttack(distanceToTarget))
            {
                if (CurrentState != EnemyState.Stagger)
                {
                    Attack();
                    Move(GetDirectionToTarget());
                }
            }
            else
            {
                if (CurrentState is EnemyState.Idle or EnemyState.Walk)
                {
                    Move(GetDirectionToTarget());
                }
            }
        }

        private float GetDistanceToTarget() => Vector2.Distance(transform.position, Target.position);

        private float GetDistanceToCurrentPoint() => Vector2.Distance(transform.position, CurrentPoint.position);

        private void ChangeWaypointIndex()
        {
            if (CurrentPointIndex >= Waypoints.Count - 1)
                CurrentPointIndex = 0;
            else
                CurrentPointIndex++;
        }

        private Vector2 GetDirectionToTarget() => ((Vector2)(Target.position - transform.position)).normalized;

        private Vector2 GetDirectionToCurrentPoint() =>
            ((Vector2)(CurrentPoint.position - transform.position)).normalized;

        protected abstract void Move(Vector2 direction);

        protected abstract void PlayMoveAnimation(Vector2 direction);

        protected abstract bool IsEnoughToChase(float distanceToTarget);

        protected abstract bool IsEnoughToAttack(float distanceToTarget);
    }
}