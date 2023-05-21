using System.Collections;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField, Range(1, 20)] private float _playerSpeed = 5f;
    private Rigidbody2D _playerRigidbody;
    private Vector3 _playerMovement;
    private float _horizontalAxis, _verticalAxis, _offset;

    public Vector3 PlayerMovementVector => _playerMovement;
    public float HorizontalAxis => _horizontalAxis;
    public float VerticalAxis => _verticalAxis;

    private void Start()
    {
        _offset = 0.6f;
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
        _verticalAxis = Input.GetAxisRaw(VERTICAL_AXIS);
        _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0) * _offset;
    }

    public void MoveCharacter(Vector3 position)
    {
        _playerRigidbody.MovePosition(position + _playerMovement.normalized * _playerSpeed * Time.deltaTime);
    }

    public IEnumerator KnockCO(float knockTime)
    {
        PlayerController.CurrentState = PlayerState.Stagger;
        yield return new WaitForSeconds(knockTime);
        _playerRigidbody.velocity = Vector2.zero;
        PlayerController.CurrentState = PlayerState.Idle;
        _playerRigidbody.velocity = Vector2.zero;
    }
}
