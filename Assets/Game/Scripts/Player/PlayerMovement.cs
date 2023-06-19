using System.Collections;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const bool _isJoystickMovement = false;

    [SerializeField, Range(1, 20)] private float _playerSpeed = 5f;
    private Joystick _joystick;
    private Rigidbody2D _playerRigidbody;
    private Vector3 _playerMovement;
    private float _horizontalAxis, _verticalAxis, _offset = 0.6f;

    public Vector3 PlayerMovementVector => _playerMovement;
    public float HorizontalAxis => _horizontalAxis;
    public float VerticalAxis => _verticalAxis;

    public Rigidbody2D PlayerRigidbody => _playerRigidbody;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isJoystickMovement)
        {
            _horizontalAxis = _joystick.Horizontal;
            _verticalAxis = _joystick.Vertical;
            _joystick.gameObject.SetActive(true);
        }
        else
        {
            _horizontalAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
            _verticalAxis = Input.GetAxisRaw(VERTICAL_AXIS);
            _joystick.gameObject.SetActive(false);
        }

        _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0) * _offset;
    }

    public void MoveCharacter(Vector3 position)
    {
        PlayerRigidbody.MovePosition(position + _playerSpeed * Time.deltaTime * _playerMovement.normalized);
    }
}
