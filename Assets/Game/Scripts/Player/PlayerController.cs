using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerState CurrentState;

    [SerializeField] private FloatValue _currentHealth;
    [SerializeField] private Signal _healthSignal;

    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private float _timeBeforeLastAttackCounter;
    private readonly float _attackCooldown = 0.4f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _timeBeforeLastAttackCounter = _attackCooldown;
    }

    private void Start()
    {
        CurrentState = PlayerState.Walk;
    }

    private void Update()
    {
        if (CurrentState == PlayerState.Interact)
            return;

        if (_timeBeforeLastAttackCounter < _attackCooldown)
        {
            _timeBeforeLastAttackCounter += Time.deltaTime;
        }
    }

    public void TryAttack()
    {
        if (CurrentState != PlayerState.Attack &&
            CurrentState != PlayerState.Stagger &&
            _timeBeforeLastAttackCounter >= _attackCooldown)
        {
            StartCoroutine(_playerAnimation.AttackCo());
            _timeBeforeLastAttackCounter = 0;
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState is PlayerState.Walk or PlayerState.Idle)
        {
            _playerAnimation.UpdateAnimation(_playerMovement.PlayerMovementVector);
            _playerMovement.MoveCharacter(transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth.RuntimeValue -= damage;
        print(_currentHealth.RuntimeValue);
        if (_currentHealth.RuntimeValue <= 0)
            StartCoroutine(GameManager.Instance.GameOver());
    }

    public IEnumerator KnockCoroutine(float knockTime, float damage)
    {
        _healthSignal.Raise(damage);
        if (_currentHealth.RuntimeValue > 0)
        {
            _healthSignal.Raise();

            CurrentState = PlayerState.Stagger;
            yield return new WaitForSeconds(knockTime);
            CurrentState = PlayerState.Idle;
            _playerMovement.PlayerRigidbody.velocity = Vector2.zero;
        }
    }
}