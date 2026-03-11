using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const float POSITION_SNAP_THRESHOLD = 0.01f;

    [Header("Component and Object")]
    [SerializeField] private Camera targetCamera;
    public GameObject followObject;
    [Header("Follow Settings")]
    public bool followAxisX = true;
    public bool followAxisY = true;
    [Tooltip("Speed of the smoothing when following the target, set to 0 for instant snapping")]
    public Vector2 positionSmoothingSpeed = Vector2.zero;
    [Header("Limit Settings")]
    [Tooltip("Minimum X position the camera can move to (left world boundary).")]
    public float leftLimit = -1000000f;
    [Tooltip("Maximum X position the camera can move to (right world boundary).")]
    public float rightLimit = 1000000f;
    [Tooltip("Maximum Y position the camera can move to (top world boundary).")]
    public float topLimit = 1000000f;
    [Tooltip("Minimum Y position the camera can move to (bottom world boundary).")]
    public float bottomLimit = -1000000f;

    // ====================================================================================================
    //                     Virtual Methods
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        if(!targetCamera) Debug.Log("targetCamera is missing, this component will not work properly");
    }

    private void FixedUpdate()
    {
        // Check target camera
        if (!targetCamera) return;
        // Get current position
        Vector3 newPosition = transform.position;
        // Follow object
        if (followObject)
        {
            if (followAxisX) newPosition.x = FollowAxisX(newPosition.x);
            if (followAxisY) newPosition.y = FollowAxisY(newPosition.y);
        }
        // Limit position
        newPosition = LimitPosition(newPosition);
        // Get new position
        transform.position = newPosition;
    }
    #endregion

    // ====================================================================================================
    //                     Follow Methods
    // ====================================================================================================
    #region Follow
    private float FollowAxisX(float currentPosition)
    {
        // Try snap position
        if (Math.Abs(currentPosition - followObject.transform.position.x) < POSITION_SNAP_THRESHOLD)
        {
            currentPosition = followObject.transform.position.x;
        }
        // Do position smoothing if enabled
        else if (positionSmoothingSpeed.x > 0)
        {
            currentPosition = Mathf.Lerp(
                currentPosition,
                followObject.transform.position.x,
                Time.fixedDeltaTime * positionSmoothingSpeed.x
            );
        }
        // Snap to position
        else currentPosition = followObject.transform.position.x;
        // Return results
        return currentPosition;
    }

    private float FollowAxisY(float currentPosition)
    {
        // Try snap position
        if (Math.Abs(currentPosition - followObject.transform.position.y) < POSITION_SNAP_THRESHOLD)
        {
            currentPosition = followObject.transform.position.y;
        }
        // Do position smoothing if enabled
        else if (positionSmoothingSpeed.y > 0)
        {
            currentPosition = Mathf.Lerp(
                currentPosition,
                followObject.transform.position.y,
                Time.fixedDeltaTime * positionSmoothingSpeed.y
            );
        }
        // Snap to position
        else currentPosition = followObject.transform.position.y;
        // Return results
        return currentPosition;
    }

    private Vector3 LimitPosition(Vector3 currentPosition)
    {
        // Get screen size in world length
        float halfHeight = targetCamera.orthographicSize;
        float halfWidth = targetCamera.aspect * halfHeight;
        // Limit camera positions
        currentPosition.x = Math.Clamp(currentPosition.x, leftLimit + halfWidth, rightLimit - halfWidth);
        currentPosition.y = Math.Clamp(currentPosition.y, bottomLimit + halfHeight, topLimit - halfHeight);
        // Return results
        return currentPosition;
    }
    #endregion
}
