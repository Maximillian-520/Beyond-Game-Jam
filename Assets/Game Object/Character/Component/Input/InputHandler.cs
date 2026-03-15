using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script is a template to handle input system.
/// Used for handling all unity's input action system.
/// Please rewrite this script as needed!
/// </summary>
public class InputHandler : MonoBehaviour
{
    public InputHandler Instance {private set; get;}

    public Vector2 moveInput = Vector2.zero;
    public bool punchInput = false;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    #endregion
    
    // ====================================================================================================
    //                     Input Functions
    // ====================================================================================================
    #region Input
    public void UpdateMoveInput(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void UpdatePunchInput(InputAction.CallbackContext ctx)
    {
        punchInput = ctx.performed;
    }
    #endregion
}
