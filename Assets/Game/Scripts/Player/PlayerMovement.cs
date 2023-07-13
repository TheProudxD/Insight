using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const bool _isJoystickMovement = false;

    [SerializeField] [Range(1, 20)] private float _playerSpeed = 5f;
    private readonly float _offset = 0.6f;
    private Joystick _joystick;
    private Vector3 _playerMovement;

    public Vector3 PlayerMovementVector => _playerMovement;
    public float HorizontalAxis { get; private set; }

    public float VerticalAxis { get; private set; }

    public Rigidbody2D PlayerRigidbody { get; private set; }

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerController.CurrentState == PlayerState.Interact)
            return;
        
        if (_isJoystickMovement)
        {
            HorizontalAxis = _joystick.Horizontal;
            VerticalAxis = _joystick.Vertical;
            _joystick.gameObject.SetActive(true);
        }

        HorizontalAxis = Input.GetAxisRaw(HORIZONTAL_AXIS);
        VerticalAxis = Input.GetAxisRaw(VERTICAL_AXIS);
        _joystick.gameObject.SetActive(false);

        _playerMovement = new Vector3(HorizontalAxis, VerticalAxis, 0) * _offset;
    }

    public void MoveCharacter(Vector3 position)
    {
        PlayerRigidbody.MovePosition(position + _playerSpeed * Time.deltaTime * _playerMovement.normalized);
    }
}