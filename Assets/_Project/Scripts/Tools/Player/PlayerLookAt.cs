using Utilites;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    private const float SPEED = 50f;

    private Vector3 _lookAtPosition;

    private void Update()
    {
        //HandleLookAtMouse();
        HandleMovement();

        /*
        if (Input.GetMouseButtonDown(0)) {
            transform.Find("Aim").GetComponent<Animator>().SetTrigger("Shoot");
            ShellParticleSystemHandler.Instance.SpawnShell(GetPosition(), (lookAtPosition - GetPosition()).normalized);
        }
        */
    }

    private void HandleLookAtMouse() => _lookAtPosition = Utils.GetMouseWorldPosition();

    public void SetLookAtPosition(Vector3 lookAtPosition) => _lookAtPosition = lookAtPosition;

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector3 lookAtDir = (_lookAtPosition - GetPosition()).normalized;
        Vector3 moveDir = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            //playerBase.PlayFeetIdleAnim(moveDir);
            //playerBase.PlayBodyHeadIdleAnim(lookAtDir);
        }
        else
        {
            //playerBase.PlayFeetWalkAnim(moveDir);
            //playerBase.PlayBodyHeadWalkAnim(lookAtDir);
            transform.position += moveDir * SPEED * Time.deltaTime;
        }
    }

    public Vector3 GetPosition() => transform.position;
}