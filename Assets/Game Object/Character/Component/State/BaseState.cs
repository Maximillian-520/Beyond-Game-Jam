using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public enum StateName
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        FALL,
        ATTACK,
        PLAYER_CONTROL, // Custom state
    }

    public StateName stateName {protected set; get;}

    [SerializeField] protected Rigidbody2D rb;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}
