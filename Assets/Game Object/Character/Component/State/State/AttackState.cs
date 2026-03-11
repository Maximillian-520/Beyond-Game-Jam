using UnityEngine;

public class AttackState : BaseState
{
    private void Awake()
    {
        // Set this state name
        stateName = StateName.ATTACK;
        // Check for rb
        if (!rb) Debug.Log("rb is missing");
    }

    public override void EnterState()
    {
        if(rb) rb.linearVelocity = Vector2.zero;
    }
    public override void ExitState(){}
    public override void UpdateState(){}
    public override void FixedUpdateState(){}
}
