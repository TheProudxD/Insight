using Utilites;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;

    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private Transform _debugHitPointTransform;
    [SerializeField] private Transform _hookshotTransform;

    private CharacterController _characterController;
    private float _cameraVerticalAngle;
    private float _characterVelocityY;
    private Camera _playerCamera;
    private CameraFov _cameraFov;
    private State _state;

    private Vector3 _characterVelocityMomentum;
    private Vector3 _hookshotPosition;
    private float _hookshotSize;

    private enum State
    {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer,
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = Utils.Camera;
        _cameraFov = _playerCamera.GetComponent<CameraFov>();
        Cursor.lockState = CursorLockMode.Locked;
        _state = State.Normal;
        //hookshotTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Normal:
                HandleCharacterLook();
                HandleCharacterMovement();
                //HandleHookshotStart();
                break;
            case State.HookshotThrown:
                HandleHookshotThrow();
                HandleCharacterLook();
                HandleCharacterMovement();
                break;
            case State.HookshotFlyingPlayer:
                HandleCharacterLook();
                HandleHookshotMovement();
                break;
        }
    }

    private void HandleCharacterLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        // Rotate the transform with the input speed around its local Y axis
        transform.Rotate(new Vector3(0f, lookX * _mouseSensitivity, 0f), Space.Self);

        // Add vertical inputs to the camera's vertical angle
        _cameraVerticalAngle -= lookY * _mouseSensitivity;

        // Limit the camera's vertical angle to min/max
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);

        // Apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        _playerCamera.transform.localEulerAngles = new Vector3(_cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float moveSpeed = 20f;

        Vector3 characterVelocity = transform.right * (moveX * moveSpeed) + transform.forward * moveZ * moveSpeed;

        if (_characterController.isGrounded)
        {
            _characterVelocityY = 0f;
            // Jump
            if (TestInputJump())
            {
                float jumpSpeed = 30f;
                _characterVelocityY = jumpSpeed;
            }
        }

        // Apply gravity to the velocity
        float gravityDownForce = -60f;
        _characterVelocityY += gravityDownForce * Time.deltaTime;


        // Apply Y velocity to move vector
        characterVelocity.y = _characterVelocityY;

        // Apply momentum
        characterVelocity += _characterVelocityMomentum;

        // Move Character Controller
        _characterController.Move(characterVelocity * Time.deltaTime);

        // Dampen momentum
        if (_characterVelocityMomentum.magnitude > 0f)
        {
            float momentumDrag = 3f;
            _characterVelocityMomentum -= _characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (_characterVelocityMomentum.magnitude < .0f)
            {
                _characterVelocityMomentum = Vector3.zero;
            }
        }
    }

    private void ResetGravityEffect() => _characterVelocityY = 0f;

    private void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward,
                    out RaycastHit raycastHit))
            {
                // Hit something
                _debugHitPointTransform.position = raycastHit.point;
                _hookshotPosition = raycastHit.point;
                _hookshotSize = 0f;
                _hookshotTransform.gameObject.SetActive(true);
                _hookshotTransform.localScale = Vector3.zero;
                _state = State.HookshotThrown;
            }
        }
    }

    private void HandleHookshotThrow()
    {
        _hookshotTransform.LookAt(_hookshotPosition);

        float hookshotThrowSpeed = 500f;
        _hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        _hookshotTransform.localScale = new Vector3(1, 1, _hookshotSize);

        if (_hookshotSize >= Vector3.Distance(transform.position, _hookshotPosition))
        {
            _state = State.HookshotFlyingPlayer;
            _cameraFov.SetCameraFov(HOOKSHOT_FOV);
        }
    }

    private void HandleHookshotMovement()
    {
        _hookshotTransform.LookAt(_hookshotPosition);

        Vector3 hookshotDir = (_hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 10f;
        float hookshotSpeedMax = 40f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, _hookshotPosition), hookshotSpeedMin,
            hookshotSpeedMax);
        float hookshotSpeedMultiplier = 5f;

        // Move Character Controller
        _characterController.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 1f;
        if (Vector3.Distance(transform.position, _hookshotPosition) < reachedHookshotPositionDistance)
        {
            // Reached Hookshot Position
            StopHookshot();
        }

        if (TestInputDownHookshot())
        {
            // Cancel Hookshot
            StopHookshot();
        }

        if (TestInputJump())
        {
            // Cancelled with Jump
            float momentumExtraSpeed = 7f;
            _characterVelocityMomentum = hookshotDir * (hookshotSpeed * momentumExtraSpeed);
            float jumpSpeed = 40f;
            _characterVelocityMomentum += Vector3.up * jumpSpeed;
            StopHookshot();
        }
    }

    private void StopHookshot()
    {
        _state = State.Normal;
        ResetGravityEffect();
        _hookshotTransform.gameObject.SetActive(false);
        _cameraFov.SetCameraFov(NORMAL_FOV);
    }

    private bool TestInputDownHookshot() => Input.GetKeyDown(KeyCode.E);

    private bool TestInputJump() => Input.GetKeyDown(KeyCode.Space);
}