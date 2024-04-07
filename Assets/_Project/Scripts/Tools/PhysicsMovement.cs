using System.Collections.Generic;
using UnityEngine;

namespace Utilites
{
    public class PhysicsMovement : MonoBehaviour
    {
        private const float MIN_MOVE_DISTANCE = 0.001f;
        private const float SHELL_RADIUS = 0.01f;

        public float MinGroundNormalY = .65f;
        public float GravityModifier = 1f;
        public Vector2 Velocity;
        public LayerMask LayerMask;

        private Vector2 _targetVelocity;
        private bool _grounded;
        private Vector2 _groundNormal;
        private Rigidbody2D _rb2d;
        private ContactFilter2D _contactFilter;
        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        private readonly List<RaycastHit2D> hitBufferList = new(16);

        private void OnEnable()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _contactFilter.useTriggers = false;
            _contactFilter.SetLayerMask(LayerMask);
            _contactFilter.useLayerMask = true;
        }

        private void Update()
        {
            _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

            if (Input.GetKey(KeyCode.Space) && _grounded)
                Velocity.y = 5;
        }

        private void FixedUpdate()
        {
            Velocity += Physics2D.gravity * (GravityModifier * Time.deltaTime);
            Velocity.x = _targetVelocity.x;

            _grounded = false;

            Vector2 deltaPosition = Velocity * Time.deltaTime;
            Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > MIN_MOVE_DISTANCE)
            {
                int count = _rb2d.Cast(move, _contactFilter, hitBuffer, distance + SHELL_RADIUS);

                hitBufferList.Clear();

                for (int i = 0; i < count; i++)
                {
                    hitBufferList.Add(hitBuffer[i]);
                }

                for (int i = 0; i < hitBufferList.Count; i++)
                {
                    Vector2 currentNormal = hitBufferList[i].normal;
                    if (currentNormal.y > MinGroundNormalY)
                    {
                        _grounded = true;
                        if (yMovement)
                        {
                            _groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0)
                    {
                        Velocity -= projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - SHELL_RADIUS;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            _rb2d.position += move.normalized * distance;
        }
    }
}