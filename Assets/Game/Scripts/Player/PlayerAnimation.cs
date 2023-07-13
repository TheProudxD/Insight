using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string XMOVE_STATE = "moveX";
    private const string YMOVE_STATE = "moveY";
    private const string ATTACKING_STATE = "attacking";
    private const string MOVING_STATE = "moving";
    private const string RECEIVE_ITEM_STATE = "receive item";

    private Animator _playerAnimator;
    //private static readonly int Property = Animator.StringToHash("receive item");
    
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
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

    public void SetReceiveItemAnimation(bool value)
    {
        _playerAnimator.SetBool(RECEIVE_ITEM_STATE,value);
    }

    public IEnumerator AttackCo()
    {
        _playerAnimator.SetBool(ATTACKING_STATE, true);
        PlayerController.CurrentState = PlayerState.Attack;
        yield return null;
        _playerAnimator.SetBool(ATTACKING_STATE, false);
        yield return new WaitForSeconds(0.3f);
        if (PlayerController.CurrentState != PlayerState.Interact)
            PlayerController.CurrentState = PlayerState.Walk;
    }
}