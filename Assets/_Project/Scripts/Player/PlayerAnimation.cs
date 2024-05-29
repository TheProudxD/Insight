using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private const string X_MOVE_STATE = "moveX";
        private const string Y_MOVE_STATE = "moveY";
        private const string ATTACKING_STATE = "attacking";
        private const string MOVING_STATE = "moving";
        private const string RECEIVE_ITEM_STATE = "receive item";

        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;
        private readonly int _xMoveStateProperty = Animator.StringToHash(X_MOVE_STATE);
        private readonly int _yMoveStateProperty = Animator.StringToHash(Y_MOVE_STATE);
        private readonly int _attackingStateProperty = Animator.StringToHash(ATTACKING_STATE);
        private readonly int _movingStateProperty = Animator.StringToHash(MOVING_STATE);
        private readonly int _receiveItemStateProperty = Animator.StringToHash(RECEIVE_ITEM_STATE);

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _playerAnimator.SetFloat(_xMoveStateProperty, 0);
            _playerAnimator.SetFloat(_yMoveStateProperty, -1);
        }
        
        private void FixedUpdate()
        {
            if (PlayerStateMachine.Current is PlayerState.Walk or PlayerState.Idle)
                UpdateMoveAnimation(_playerMovement.PlayerMovementVector);
        }

        private void UpdateMoveAnimation(Vector3 playerMovement)
        {
            if (playerMovement == Vector3.zero)
            {
                _playerAnimator.SetBool(_movingStateProperty, false);
                return;
            }

            if (Mathf.Abs(playerMovement.x) > Mathf.Abs(playerMovement.y))
            {
                _playerAnimator.SetFloat(_xMoveStateProperty, playerMovement.x * (1 / Mathf.Abs(playerMovement.x)));
                _playerAnimator.SetFloat(_yMoveStateProperty, 0);
            }
            else
            {
                _playerAnimator.SetFloat(_xMoveStateProperty, 0);
                _playerAnimator.SetFloat(_yMoveStateProperty, playerMovement.y * (1 / Mathf.Abs(playerMovement.y)));
            }

            _playerAnimator.SetBool(_movingStateProperty, true);
        }

        public void SetReceiveItemAnimation(bool value) => _playerAnimator.SetBool(_receiveItemStateProperty, value);

        public void SetAttackingAnimation(bool value) => _playerAnimator.SetBool(_attackingStateProperty, value);
    }
}