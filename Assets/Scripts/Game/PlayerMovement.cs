using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Attack,
    Interact
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float _playerSpeed = 5f;
    private PlayerState _currentState;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private Vector3 _playerMovement;
    private float _horizontalAxis, _verticalAxis, _offset;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const string XMOVE_STATE = "moveX";
    private const string YMOVE_STATE = "moveY";
    private const string ATTACKING_STATE = "attacking";
    private const string MOVING_STATE = "moving";
    private void Start()
    {
        _offset = 0.6f;
        _currentState = PlayerState.Walk;

        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _playerAnimator.WriteDefaultValues();
        //_playerAnimator.SetFloat(XMOVE_STATE, 0);
        //_playerAnimator.SetFloat(YMOVE_STATE, -1);
    }
    private void Update()
    {
        _horizontalAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
        _verticalAxis = Input.GetAxisRaw(VERTICAL_AXIS);
        _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0) * _offset;
        if (Input.GetButtonDown("Attack") && _currentState != PlayerState.Attack)
            StartCoroutine(AttackCo());
    }
    private void FixedUpdate()
    {
        if (_currentState == PlayerState.Walk)
            UpdateAnimation();
    }
    private IEnumerator AttackCo()
    {
        _playerAnimator.SetBool(ATTACKING_STATE, true);
        _currentState = PlayerState.Attack;
        yield return null;
        _playerAnimator.SetBool(ATTACKING_STATE, false);
        yield return new WaitForSeconds(0.3f);
        _currentState = PlayerState.Walk;
    }
    private void UpdateAnimation()
    {
        if (_playerMovement != Vector3.zero)
        {
            MoveCharacter(transform.position);

            _playerAnimator.SetFloat(XMOVE_STATE, _horizontalAxis);
            _playerAnimator.SetFloat(YMOVE_STATE, _verticalAxis);
            _playerAnimator.SetBool(MOVING_STATE, true);
        }
        else
        {
            _playerAnimator.SetBool(MOVING_STATE, false);
        }
    }
    public void MoveCharacter(Vector3 position)
    {
        _playerRigidbody.MovePosition(position + _playerMovement.normalized * _playerSpeed * Time.deltaTime);
    }
}
