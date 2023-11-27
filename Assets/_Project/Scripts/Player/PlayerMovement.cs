using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const bool IS_JOYSTICK_MOVEMENT = false;

        [SerializeField, Range(1, 20)] private float _playerSpeed = 5f;
        public Vector3 PlayerDirectionVector { get; private set; } = Vector3.down;
        public Vector3 PlayerMovementVector => _playerMovement;
        public Rigidbody2D PlayerRigidbody { get; private set; }

        private Joystick _joystick;
        private Vector3 _playerMovement;
        private float _horizontalAxis;
        private float _verticalAxis;

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
            PlayerRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (PlayerController.CurrentState == PlayerState.Interact)
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
        
        private void GetDirection()
        {
            if (_horizontalAxis != 0)
                PlayerDirectionVector = _horizontalAxis > 0 ? Vector2.right : Vector2.left;
            else if (_verticalAxis != 0)
                PlayerDirectionVector = _verticalAxis > 0 ? Vector2.up : Vector2.down;
        }

        public void MoveCharacter(Vector3 position) =>
            PlayerRigidbody.MovePosition(position + _playerSpeed * Time.deltaTime * _playerMovement.normalized);
    }
}