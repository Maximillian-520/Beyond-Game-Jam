using System;
using UnityEngine;

public class WalkState : BaseState
{
    // Speed and acceleration
    public bool isMovementSnap = true; // Snap the movement velocity, disable this would make movement uses acceleration
    public float moveSpeed = 1;
    public float moveAcceleration = 2;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        // Set this state name
        stateName = StateName.WALK;
        // Check for rb
        if (!rb) Debug.Log("rb is missing");
    }

    public override void EnterState(){}
    public override void ExitState(){}
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        if (rb)
        {
            if (isMovementSnap) rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            else
            {
                Vector2 targetVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
                float acceleration =  moveAcceleration * Time.fixedDeltaTime;
                rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, targetVelocity.x, acceleration);
                rb.linearVelocityY = Mathf.MoveTowards(rb.linearVelocityY, targetVelocity.y, acceleration);
            }
        }
    }
}
