using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Attack,
    Interact,
    Stagger
}

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerState CurrentState;

    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && CurrentState != PlayerState.Attack && CurrentState != PlayerState.Stagger)
            StartCoroutine(_playerAnimation.AttackCo());
    }

    private void Start()
    {
        CurrentState = PlayerState.Walk;
    }

    private void FixedUpdate()
    {
        if (CurrentState is PlayerState.Walk || CurrentState is PlayerState.Idle)
        {
            _playerAnimation.UpdateAnimation(_playerMovement.PlayerMovementVector, _playerMovement.HorizontalAxis, _playerMovement.VerticalAxis);
            _playerMovement.MoveCharacter(transform.position);
        }
    }
}
