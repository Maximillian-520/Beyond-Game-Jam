using UnityEngine;
using UnityEngine.EventSystems;

public class MainButton : MonoBehaviour,
IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Component and Object")]
    [SerializeField] private UnityEngine.UI.Image image;
    [Tooltip("Used for offseting object")]
    [SerializeField] private RectTransform content;
    [Header("Texture")]
    [Tooltip("Set image texture to value when button normal")]
    [SerializeField] private Sprite normalTexture;
    [Tooltip("Set image texture to value when button hover")]
    [SerializeField] private Sprite hoverTexture;
    [Tooltip("Set image texture to value when button pressed")]
    [SerializeField] private Sprite pressedTexture;
    [Header("Offset")]
    [Tooltip(
        "Offset content by value when button normal." +
        "Only works when content object is set."
    )]
    [SerializeField] private Vector2 normalContentOffset;
    [Tooltip(
        "Offset content by value when button hover." +
        "Only works when content object is set."
    )]
    [SerializeField] private Vector2 hoverContentOffset;
    [Tooltip(
        "Offset content by value when button pressed." +
        "Only works when content object is set."
    )]
    [SerializeField] private Vector2 pressedContentOffset;

    private Vector2 currentOffset = Vector2.zero;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(image, "image is missing");
        Debug.Assert(normalTexture, "normalTexture is empty");
        Debug.Assert(hoverTexture, "hoverTexture is empty");
        Debug.Assert(pressedTexture, "pressedTexture is empty");
    }
    #endregion

    // ====================================================================================================
    //                     Pointer Functions
    // ====================================================================================================
    #region Pointer
    public void OnPointerEnter(PointerEventData eventData){
        // Texture
        image.sprite = hoverTexture;
        // Offset
        if (content)
        {
            content.transform.position -= new Vector3(
                currentOffset.x, currentOffset.y, 0
            );
            content.transform.position += new Vector3(
                hoverContentOffset.x, hoverContentOffset.y, 0
            );
            currentOffset = hoverContentOffset;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        // Texture
        image.sprite = normalTexture;
        // Offset
        if (content)
        {
            content.transform.position -= new Vector3(
                currentOffset.x, currentOffset.y, 0
            );
            content.transform.position += new Vector3(
                normalContentOffset.x, normalContentOffset.y, 0
            );
            currentOffset = normalContentOffset;
        }
    }

    public void OnPointerUp(PointerEventData eventData){
        // Texture
        image.sprite = hoverTexture;
        // Offset
        if (content)
        {
            content.transform.position -= new Vector3(
                currentOffset.x, currentOffset.y, 0
            );
            content.transform.position += new Vector3(
                hoverContentOffset.x, hoverContentOffset.y, 0
            );
            currentOffset = hoverContentOffset;
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        // Texture
        image.sprite = pressedTexture;
        // Offset
        if (content)
        {
            content.transform.position -= new Vector3(
                currentOffset.x, currentOffset.y, 0
            );
            content.transform.position += new Vector3(
                pressedContentOffset.x, pressedContentOffset.y, 0
            );
            currentOffset = pressedContentOffset;
        }
    }
    #endregion
}
