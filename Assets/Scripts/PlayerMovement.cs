using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;

    private Rigidbody2D _playerRigidbody;
    private float _horizontalAxis, _verticalAxis;
    private void Start()
    {
        _playerRigidbody=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(_horizontalAxis, _verticalAxis, 0);
        _playerRigidbody.MovePosition(transform.position+movement * _playerSpeed * Time.deltaTime);
    }
}
