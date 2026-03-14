using UnityEngine;
using UnityEngine.UI;

public class Sponge : MonoBehaviour
{
    public static Sponge Instance {private set; get;}

    [Header("Component and Object")]
    [SerializeField] private Image image;
    [Header("Input")]
    [SerializeField] private int buttonInputNumber = 0; // Idk what this is
    [Header("SFX")]
    [SerializeField] private string sfxName = "Sponge";
    [SerializeField] private float sfxminimumVelocityThreshold = 10.0f;
    [SerializeField] private float sfxBufferTime = 0.2f;

    private Vector3 previousPosition;
    private float sfxBufferTimer = 0.0f;

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
        // Update scrubbing
        isScrubbing = Input.GetMouseButton(buttonInputNumber);
        // Update buffer timer
        if (sfxBufferTimer > 0) sfxBufferTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            // Update position
            transform.position = Input.mousePosition;
            // Check is sfx can play
            Vector3 distanceVector = transform.position - previousPosition;
            float velocity = distanceVector.magnitude / Time.fixedDeltaTime;
            if (isScrubbing && velocity >= sfxminimumVelocityThreshold)
            {
                AudioManager.Instance.PlaySFXLooping(sfxName);
                sfxBufferTimer = sfxBufferTime;
            }
            else if (sfxBufferTimer > 0) AudioManager.Instance.PlaySFXLooping(sfxName);
            else AudioManager.Instance.StopSFXLooping();
            // Update previous position
            previousPosition = Input.mousePosition;
        }
    }
    #endregion
}
