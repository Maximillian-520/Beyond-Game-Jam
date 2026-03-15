using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // This script is a template to handle input system.
    // Please replace this script as needed!
    // TODO: Create local manager

    public Vector2 moveInput = Vector2.zero;

    public void UpdateMoveInput(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
}
