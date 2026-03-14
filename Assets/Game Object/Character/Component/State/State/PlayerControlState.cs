using System;
using UnityEngine;

// Custom state for player, an advance movement script
public class PlayerControlState : BaseState
{
    [Header("Component and Object")]
    [SerializeField] private PlayerSprite playerSprite;
    [SerializeField] private InputHandler inputHandler;
    [Header("Move")]
    public float maxWalkSpeed = 14f;
    public float speedAcceleration = 120f;
    [Header("SFX")]
    [SerializeField] private string moveSfxName = "Walk";

    private int facingDirection = 1;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Awake()
    {
        // Set this state name
        stateName = StateName.PLAYER_CONTROL;
        // Check for rb
        if (!rb) Debug.Log("rb is missing");
    }

    private void Start()
    {
        // Assertion Check
        Debug.Assert(playerSprite, "playerSprite is missing");
        Debug.Assert(inputHandler, "inputHandler is missing");
    }
    #endregion

    // ====================================================================================================
    //                     State Functions
    // ====================================================================================================
    #region State
    public override void EnterState(){}
    public override void ExitState()
    {
        playerSprite.DoIdle();
        AudioManager.Instance.StopSFXLooping();
    }
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        HandleMovement();
        HandleFacingDirection();
        HandleAnimation();
        HandleSFX();
    }
    #endregion

    // ====================================================================================================
    //                     Handle Functions
    // ====================================================================================================
    #region Handle
    private void HandleMovement()
    {
        Vector2 targetVelocity = inputHandler.moveInput.normalized * maxWalkSpeed;
        rb.linearVelocityX = Mathf.MoveTowards(
            rb.linearVelocityX, targetVelocity.x, Time.deltaTime * speedAcceleration
        );
        rb.linearVelocityY = Mathf.MoveTowards(
            rb.linearVelocityY, targetVelocity.y, Time.deltaTime * speedAcceleration
        );
    }

    public void HandleFacingDirection()
    {
        int newDirection = Math.Sign(inputHandler.moveInput.x);
        // Check if flip is needed
        if (newDirection == 0 || newDirection == facingDirection) return;
        // Update scale of sprite object
        Vector3 newScale = playerSprite.transform.localScale;
        newScale.x *= -1;
        playerSprite.transform.localScale = newScale;
        // Update facing direction
        facingDirection = newDirection;
    }

    private void HandleAnimation()
    {
        if (inputHandler.moveInput != Vector2.zero) playerSprite.DoWalk();
        else playerSprite.DoIdle();
    }

    private void HandleSFX()
    {
        if (inputHandler.moveInput != Vector2.zero) AudioManager.Instance.PlaySFXLooping(moveSfxName);
        else AudioManager.Instance.StopSFXLooping();
    }
    #endregion

    // // ====================================================================================================
    // //                     Helper Functions
    // // ====================================================================================================
    // #region Helper
    // private Vector3 GetMousePosition()
    // {
    //     Vector3 screenMousePosition = Input.mousePosition;
    //     Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
    //         screenMousePosition.x,
    //         screenMousePosition.y,
    //         0
    //     ));
    //     worldMousePosition.y -= 1.1f;
    //     return worldMousePosition;
    // }

    // private Vector3 GetMouseDirection()
    // {
    //     Vector3 distanceVector = GetMousePosition() - transform.position;
    //     distanceVector.z = 1;
    //     return distanceVector.normalized;
    // }

    // private float GetAngleFromTwoPoints(Vector2 fromPoint, Vector2 toPoint)
    // {
    //     Vector2 direction = toPoint - fromPoint;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     return angle;
    // }
    // #endregion
}
