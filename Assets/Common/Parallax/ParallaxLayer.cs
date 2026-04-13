using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("How much this layer moves relative to the camera (0 = no movement, 1 = same as camera)")]
    [SerializeField] private Vector2 parallaxFactor = new Vector2(1, 0);
    [Header("Scroll Settings")]
    [Tooltip(
        "Distance from the anchor position before the layer repeats (loops)." +
        "Will not repeat if set to 0."
    )]
    [SerializeField] private Vector2 repeatSize = new Vector2(0, 0);
    [Tooltip("Automatic scrolling speed of the layer in units per second")]
    [SerializeField] private Vector2 autoscrollSpeed = new Vector2(0, 0);

    private Vector3 anchorPosition;

    // ====================================================================================================
    //                     Virtual Methods
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        anchorPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        // Calculate autoscroll
        Vector3 autoScrollDistance = Vector3.zero;
        if(autoscrollSpeed.x != 0) autoScrollDistance.x += autoscrollSpeed.x;
        if(autoscrollSpeed.y != 0) autoScrollDistance.x += autoscrollSpeed.y;
        if(parallaxFactor.x != 0) autoScrollDistance.x /= parallaxFactor.x;
        if(parallaxFactor.y != 0) autoScrollDistance.x /= parallaxFactor.y;
        // Apply autoscroll
        autoScrollDistance *= Time.fixedDeltaTime;
        if(autoScrollDistance != Vector3.zero) MoveLayer(autoscrollSpeed);
    }

    public void MoveLayer(Vector3 moveDistance)
    {
        // Calculate new position
        Vector3 newPosition = transform.localPosition;
        newPosition.x -= moveDistance.x * parallaxFactor.x;
        newPosition.y -= moveDistance.y * parallaxFactor.y;
        // Set new position
        transform.localPosition = newPosition;
    }

    private void LateUpdate()
    {
        // Get current position
        Vector3 currentPosition = transform.localPosition;
        // Check for repeat
        if (repeatSize.x > 0) currentPosition.x = DoHorizontalRepeat(currentPosition.x);
        if (repeatSize.y > 0) currentPosition.y = DoVerticalRepeat(currentPosition.y);
        // Set new position
        transform.localPosition = currentPosition;
    }
    #endregion

    // ====================================================================================================
    //                     Parallax Methods
    // ====================================================================================================
    #region Parallax
    private float DoHorizontalRepeat(float currentPositionX)
    {
        // Get distance and limit
        float distanceX = currentPositionX - anchorPosition.x;
        float limitX = repeatSize.x;
        // Check if went beyond the limit
        if (distanceX > limitX) currentPositionX -= repeatSize.x;
        else if (distanceX < -limitX) currentPositionX += repeatSize.x;
        // Return results
        return currentPositionX;
    }

    private float DoVerticalRepeat(float currentPositionY)
    {
        // Get distance and limit
        float distanceX = currentPositionY - anchorPosition.x;
        float limitX = repeatSize.x;
        // Check if went beyond the limit
        if (distanceX > limitX) currentPositionY -= repeatSize.x;
        else if (distanceX < -limitX) currentPositionY += repeatSize.x;
        // Return results
        return currentPositionY;
    }
    #endregion
}
