using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;

    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private Vector3 _playerMovement;
    private float _horizontalAxis, _verticalAxis;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _playerMovement = new Vector3(_horizontalAxis, _verticalAxis, 0);

        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        if (_playerMovement != Vector3.zero)
        {
            MoveCharacter();

            _playerAnimator.SetFloat("moveX", _horizontalAxis);
            _playerAnimator.SetFloat("moveY", _verticalAxis);
            _playerAnimator.SetBool("moving", true);
        }
        else
        {
            _playerAnimator.SetBool("moving", false);
        }
    }
    private void MoveCharacter()
    {
        _playerRigidbody.MovePosition(transform.position + _playerMovement * _playerSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            SceneManager.LoadScene(3);
        }
    }
}
