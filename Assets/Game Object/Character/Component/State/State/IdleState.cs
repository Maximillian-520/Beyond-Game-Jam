using UnityEngine;

public class IdleState : BaseState
{
    // Acceleration
    public bool isMovementSnap = true; // Snap the movement velocity, disable this would make movement uses acceleration
    public float moveDecceleration = 4;

    private void Awake()
    {
        // Set this state name
        stateName = StateName.IDLE;
        // Check for rb
        if (!rb) Debug.Log("rb is missing");
    }

    public override void EnterState()
    {
        if (isMovementSnap)  rb.linearVelocity = Vector2.zero;
    }
    public override void ExitState(){}
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        if (rb && !isMovementSnap)
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, moveDecceleration * Time.fixedDeltaTime);
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, 0, moveDecceleration * Time.fixedDeltaTime);
        }
    }
}
