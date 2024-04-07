using Utilites;
using UnityEngine;

public class PlayerPositionMovement : MonoBehaviour
{
    private const float SPEED = 50f;
    private Vector3 targetPosition;

    private void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0)) 
            SetTargetPosition(Utils.GetMouseWorldPosition());
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;

            //playerBase.PlayMoveAnim(moveDir);
            transform.position += moveDir * (SPEED * Time.deltaTime);
        }
        else
        {
            //playerBase.PlayIdleAnim();
        }
    }

    public Vector3 GetPosition() => transform.position;

    public void SetTargetPosition(Vector3 targetPosition)
    {
        targetPosition.z = 0f;
        this.targetPosition = targetPosition;
    }
}