using UnityEngine;

public class FreeflyCamera : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotSpeed = 0.1f;

    private Vector2? _prevMousePos;
    private void Update()
    {
       KeyboardControl();
       MouseControl();
    }

    private void KeyboardControl()
    {
        var dv = _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * dv;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * dv;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * dv;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * dv;
        }
    }

    private void MouseControl()
    {
        if (Input.GetMouseButton(1))
        {
            if (_prevMousePos.HasValue)
            {
                var dp =  (Vector2) Input.mousePosition - _prevMousePos.Value;
                var euler = transform.localEulerAngles;
                var angleY = euler.y + _rotSpeed * dp.x;
                var angleX = euler.x - _rotSpeed * dp.y;
                transform.localEulerAngles = new Vector3(angleX, angleY);
                _prevMousePos = Input.mousePosition;
            }
            else
            {
                _prevMousePos = Input.mousePosition;
            }
        }
        else
        {
            _prevMousePos = null;
        }
    }
}