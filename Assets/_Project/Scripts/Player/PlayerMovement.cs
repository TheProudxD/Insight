using _Project.Scripts.Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const bool IS_JOYSTICK_MOVEMENT = false;

        [Inject] private PlayerEntitySpecs _playerEntitySpecs;
        [SerializeField] private Joystick _joystick;

        public Vector3 PlayerDirectionVector { get; private set; } = Vector3.down;
        public Vector3 PlayerMovementVector => _playerMovement;
        public Rigidbody2D PlayerRigidbody { get; private set; }

        private Vector3 _playerMovement;
        private float _horizontalAxis;
        private float _verticalAxis;

        private void Awake()
        {
            PlayerRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (PlayerCurrentState.Current == PlayerState.Interact)
                return;

            if (IS_JOYSTICK_MOVEMENT)
#pragma warning disable CS0162 // Unreachable code detected
            {
                _horizontalAxis = _joystick.Horizontal;
                _verticalAxis = _joystick.Vertical;
                _joystick.gameObject.SetActive(true);
            }
#pragma warning restore CS0162 // Unreachable code detected
            else
            {
                _horizontalAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
                _verticalAxis = Input.GetAxisRaw(VERTICAL_AXIS);
                _joystick.gameObject.SetActive(false);
            }

            GetDirection();

            _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0);
        }

        private void FixedUpdate()
        {
            if (PlayerCurrentState.Current is PlayerState.Walk or PlayerState.Idle)
                MoveCharacter(transform.position);
        }

        private void GetDirection()
        {
            if (_horizontalAxis != 0)
                PlayerDirectionVector = _horizontalAxis > 0 ? Vector2.right : Vector2.left;
            else if (_verticalAxis != 0)
                PlayerDirectionVector = _verticalAxis > 0 ? Vector2.up : Vector2.down;
        }

        private void MoveCharacter(Vector3 position) =>
            PlayerRigidbody.MovePosition(position + _playerEntitySpecs.MoveSpeed * Time.deltaTime * _playerMovement.normalized);
    }
}