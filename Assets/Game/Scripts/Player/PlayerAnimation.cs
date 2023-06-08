using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string XMOVE_STATE = "moveX";
    private const string YMOVE_STATE = "moveY";
    private const string ATTACKING_STATE = "attacking";
    private const string MOVING_STATE = "moving";

    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetFloat(XMOVE_STATE, 0);
        _playerAnimator.SetFloat(YMOVE_STATE, -1);
    }

    public void UpdateAnimation(Vector3 playerMovement, float horizontalAxis, float verticalAxis)
    {
        if (playerMovement != Vector3.zero)
        {
            _playerAnimator.SetFloat(XMOVE_STATE, horizontalAxis);
            _playerAnimator.SetFloat(YMOVE_STATE, verticalAxis);
            _playerAnimator.SetBool(MOVING_STATE, true);
        }
        else
        {
            _playerAnimator.SetBool(MOVING_STATE, false);
        }
    }

    public IEnumerator AttackCo()
    {
        _playerAnimator.SetBool(ATTACKING_STATE, true);
        PlayerController.CurrentState = PlayerState.Attack;
        yield return null;
        _playerAnimator.SetBool(ATTACKING_STATE, false);
        yield return new WaitForSeconds(0.3f);
        PlayerController.CurrentState = PlayerState.Walk;
    }
}
