using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private PlayerEntitySpecs _playerEntitySpecs;
        [Inject] private IInputReader _inputReader;

        public Vector2 PlayerMovementVector => _playerMovement;
        public Rigidbody2D PlayerRigidbody { get; private set; }

        private Vector2 _playerMovement;
        private Vector2 _faceDirection = Vector2.down;

        private void Awake() => PlayerRigidbody = GetComponent<Rigidbody2D>();

        private void Update()
        {
            if (PlayerStateMachine.Current == PlayerState.Interact)
                return;
            
            ReadInput();
        }

        private void FixedUpdate()
        {
            if (PlayerStateMachine.Current is PlayerState.Walk or PlayerState.Idle)
                MoveCharacter(transform.position);
        }

        private void ReadInput()
        {
            var inputDirection = _inputReader.GetInputDirection();
            var horizontalAxis = inputDirection.x;
            var verticalAxis = inputDirection.y;

            _playerMovement = new Vector2(horizontalAxis, verticalAxis);
            if (_playerMovement != Vector2.zero)
            {
                _faceDirection = _playerMovement;
            }
        }

        private void MoveCharacter(Vector2 position) =>
            PlayerRigidbody.MovePosition(position +
                                         _playerEntitySpecs.MoveSpeed * Time.deltaTime * _playerMovement.normalized);

        public Vector2 GetFaceDirection()
        {
            var direction = new Vector2();
            direction += _faceDirection.x > 0 ? Vector2.right : _faceDirection.x != 0 ? Vector2.left : Vector2.zero;
            direction += _faceDirection.y > 0 ? Vector2.up : _faceDirection.y != 0 ? Vector2.down : Vector2.zero;
            return direction;
        }
    }
}