using UnityEngine;
using UnityEngine.UI;

public class Sponge : MonoBehaviour
{
    public static Sponge Instance {private set; get;}

    [Header("Component and Object")]
    [SerializeField] private Image image;
    [Header("Input")]
    [SerializeField] private int buttonInputNumber = 0; // Idk what this is

    public bool Active
    {
        set
        {
            isActive = value;
            transform.position = Input.mousePosition;
            image.gameObject.SetActive(isActive);
        }
        get{return isActive;}
    }
    public bool Scrubbing
    {
        private set{isScrubbing = value;}
        get{return isActive && isScrubbing;}
    }

    private bool isActive = false;
    private bool isScrubbing = false;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        isScrubbing = Input.GetMouseButton(buttonInputNumber);
    }

    private void FixedUpdate()
    {
        if (isActive) transform.position = Input.mousePosition;
    }
    #endregion
}
