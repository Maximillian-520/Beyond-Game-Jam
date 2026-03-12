using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [Header("Component and Object")]
    [Tooltip("Main camera for the parallax")]
    [SerializeField] private Camera followCamera;
    [Header("Parallax")]
    public bool isEnabled = true;

    // Parallax layers that will be used for this contorller.
    private List<ParallaxLayer> parallaxLayerList = new List<ParallaxLayer>();
    // Tracks camera previous position
    private Vector3 previousPosition;

    // ====================================================================================================
    //                     Virtual Methods
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Find all parallax layer child
        parallaxLayerList.Clear();
        for (int index = 0; index < transform.childCount; index++)
        {
            ParallaxLayer parallaxLayer = transform.GetChild(index).GetComponent<ParallaxLayer>();
            if (parallaxLayer != null)
            {
                parallaxLayerList.Add(parallaxLayer);
            }
        }
        if (parallaxLayerList.Count == 0) Debug.Log("Couldn't find any child with ParallaxLayer");
        // Follow camera position
        transform.position = new Vector3(
            followCamera.transform.position.x,
            followCamera.transform.position.y,
            transform.position.z
        );
    }

    private void FixedUpdate()
    {
        // Check enable
        if (!isEnabled) return;
        // Check if position moved
        Vector3 currentPosition = followCamera.transform.position;
        if(currentPosition != previousPosition)
        {
            // Call move function to all layers
            foreach (ParallaxLayer parallaxLayer in parallaxLayerList)
            {
                parallaxLayer.MoveLayer(currentPosition - previousPosition);
            }
            // Update position variable
            previousPosition = currentPosition;
            // Follow camera position
            transform.position = new Vector3(
                followCamera.transform.position.x,
                followCamera.transform.position.y,
                transform.position.z
            );
        }
    }
    #endregion
}
