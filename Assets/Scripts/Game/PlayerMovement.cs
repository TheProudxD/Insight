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
    [SerializeField] private float _playerSpeed;
    private PlayerState _currentState;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private Vector3 _playerMovement;
    private float _horizontalAxis, _verticalAxis, _offset;
    private void Start()
    {
        _offset = 0.6f;
        _currentState = PlayerState.Walk;

        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        _horizontalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");
        _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0)*_offset;
        if (Input.GetButtonDown("Attack") && _currentState != PlayerState.Attack)
            StartCoroutine(AttackCo());
    }
    private void FixedUpdate()
    {
        if (_currentState==PlayerState.Walk)
            UpdateAnimation();
    }
    private IEnumerator AttackCo()
    {
        _playerAnimator.SetBool("attacking", true);
        _currentState = PlayerState.Attack;
        yield return null;
        _playerAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        _currentState = PlayerState.Walk;
    }
    private void UpdateAnimation()
    {
        if (_playerMovement != Vector3.zero)
        {
            MoveCharacter(transform.position);

            _playerAnimator.SetFloat("moveX", _horizontalAxis);
            _playerAnimator.SetFloat("moveY", _verticalAxis);
            _playerAnimator.SetBool("moving", true);
        }
        else
        {
            _playerAnimator.SetBool("moving", false);
        }
    }
    public void MoveCharacter(Vector3 position)
    {
        _playerRigidbody.MovePosition(position + _playerMovement * _playerSpeed * Time.deltaTime);
    }
}
