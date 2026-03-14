using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCompletedIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Component and Object")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [Header("Icon and Text and Color")]
    [SerializeField] private Sprite emptyIconTexture;
    [SerializeField] private Sprite filledIconTexture;
    [SerializeField] private string emptyText;
    [SerializeField] private string filledText;
    [SerializeField] private Color emptyTextColor = Color.red;
    [SerializeField] private Color filledTextColor = Color.green;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(iconImage, "iconImage is missing");
        Debug.Assert(descriptionText, "descriptionText is missing");
        Debug.Assert(emptyIconTexture, "emptyIconTexture is empty");
        Debug.Assert(filledIconTexture, "filledIconTexture is empty");
        Debug.Assert(emptyText == "", "emptyText is empty");
        Debug.Assert(filledText == "", "filledText is empty");
        // Set icon image and description
        GameData gameData = GameSaveHandler.LoadGameData();
        if (!gameData.isEmpty && gameData.isGameDefeated)
        {
            iconImage.sprite = filledIconTexture;
            descriptionText.text = filledText;
            descriptionText.color = filledTextColor;
        }
        else
        {
            iconImage.sprite = emptyIconTexture;
            descriptionText.text = emptyText;
            descriptionText.color = emptyTextColor;
        }
        // Initialize
        descriptionText.gameObject.SetActive(false);
    }
    #endregion

    // ====================================================================================================
    //                     Pointer Functions
    // ====================================================================================================
    #region Pointer
    public void OnPointerEnter(PointerEventData eventData) {descriptionText.gameObject.SetActive(true);}

    public void OnPointerExit(PointerEventData eventData) {descriptionText.gameObject.SetActive(false);}
    #endregion
}
