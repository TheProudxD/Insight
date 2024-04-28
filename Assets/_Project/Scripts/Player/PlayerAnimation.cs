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
        private readonly int XMoveStateProperty = Animator.StringToHash(X_MOVE_STATE);
        private readonly int YMoveStateProperty = Animator.StringToHash(Y_MOVE_STATE);
        private readonly int AttackingStateProperty = Animator.StringToHash(ATTACKING_STATE);
        private readonly int MovingStateProperty = Animator.StringToHash(MOVING_STATE);
        private readonly int ReceiveItemStateProperty = Animator.StringToHash(RECEIVE_ITEM_STATE);

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _playerAnimator.SetFloat(XMoveStateProperty, 0);
            _playerAnimator.SetFloat(YMoveStateProperty, -1);
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
                _playerAnimator.SetBool(MovingStateProperty, false);
                return;
            }

            if (Mathf.Abs(playerMovement.x) > Mathf.Abs(playerMovement.y))
            {
                _playerAnimator.SetFloat(XMoveStateProperty, playerMovement.x * (1 / Mathf.Abs(playerMovement.x)));
                _playerAnimator.SetFloat(YMoveStateProperty, 0);
            }
            else
            {
                _playerAnimator.SetFloat(XMoveStateProperty, 0);
                _playerAnimator.SetFloat(YMoveStateProperty, playerMovement.y * (1 / Mathf.Abs(playerMovement.y)));
            }

            _playerAnimator.SetBool(MovingStateProperty, true);
        }

        public void SetReceiveItemAnimation(bool value) => _playerAnimator.SetBool(ReceiveItemStateProperty, value);

        public void SetAttackingAnimation(bool value) => _playerAnimator.SetBool(AttackingStateProperty, value);
    }
}