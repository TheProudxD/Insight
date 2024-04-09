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

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _playerAnimator.SetFloat(X_MOVE_STATE, 0);
            _playerAnimator.SetFloat(Y_MOVE_STATE, -1);
        }
        
        private void FixedUpdate()
        {
            if (PlayerCurrentState.Current is PlayerState.Walk or PlayerState.Idle)
                UpdateMoveAnimation(_playerMovement.PlayerMovementVector);
        }

        public void UpdateMoveAnimation(Vector3 playerMovement)
        {
            if (playerMovement == Vector3.zero)
            {
                _playerAnimator.SetBool(MOVING_STATE, false);
                return;
            }

            if (Mathf.Abs(playerMovement.x) > Mathf.Abs(playerMovement.y))
            {
                _playerAnimator.SetFloat(X_MOVE_STATE, playerMovement.x * (1 / Mathf.Abs(playerMovement.x)));
                _playerAnimator.SetFloat(Y_MOVE_STATE, 0);
            }
            else
            {
                _playerAnimator.SetFloat(X_MOVE_STATE, 0);
                _playerAnimator.SetFloat(Y_MOVE_STATE, playerMovement.y * (1 / Mathf.Abs(playerMovement.y)));
            }

            _playerAnimator.SetBool(MOVING_STATE, true);
        }

        public void SetReceiveItemAnimation(bool value) => _playerAnimator.SetBool(RECEIVE_ITEM_STATE, value);

        public void SetAttackingAnimation(bool value) => _playerAnimator.SetBool(ATTACKING_STATE, value);
    }
}