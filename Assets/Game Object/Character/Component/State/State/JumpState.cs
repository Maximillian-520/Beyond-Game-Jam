using System;
using UnityEngine;

public class JumpState : BaseState
{
    // Speed and acceleration
    public bool isMovementSnap = true; // Snap the movement velocity, disable this would make movement uses acceleration
    public float moveSpeed = 1;
    public float moveAcceleration = 2;
    [HideInInspector] public int moveDirection = 0;
    public float jumpSpeed = 4;

    private void Awake()
    {
        // Set this state name
        stateName = StateName.JUMP;
        // Check for rb
        if (!rb) Debug.Log("rb is missing");
    }

    public override void EnterState()
    {
        if(rb) rb.linearVelocityY = jumpSpeed;
    }
    public override void ExitState(){}
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        if (rb)
        {
            if (isMovementSnap) rb.linearVelocityX = moveDirection * moveSpeed;
            else
            {
                float targetVelocity = moveDirection * moveSpeed;
                float acceleration =  moveAcceleration * Time.fixedDeltaTime;
                rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, targetVelocity, acceleration);
            }
        }
        // NOTE: Gravity fall speed uses RigidBody gravity system
    }
}
