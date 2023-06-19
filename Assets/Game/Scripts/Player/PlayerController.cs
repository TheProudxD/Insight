using System.Collections.Generic;
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

    [SerializeField] private FloatValue _currentHealth;
    [SerializeField] private Signal _healthSignal;
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

    public void TakeDamage(float damage)
    {
        _currentHealth.RuntimeValue -= damage;
        print(_currentHealth.RuntimeValue);
    }

    public System.Collections.IEnumerator KnockCO(float knockTime, float _damage)
    {
        _healthSignal.Raise(_damage);
        if (_currentHealth.RuntimeValue > 0)
        {
            _healthSignal.Raise();

            CurrentState = PlayerState.Stagger;
            yield return new WaitForSeconds(knockTime);
            _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
            CurrentState = PlayerState.Idle;
            _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
        }
    }
}
