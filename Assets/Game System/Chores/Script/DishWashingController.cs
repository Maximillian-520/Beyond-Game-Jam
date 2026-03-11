using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DishWashingController : MonoBehaviour
{
    public event EventHandler OnMinigameEnded;

    [Header("Component and Object")]
    [SerializeField] private DishWashingAnimation dishWashingAnimation;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform plateParent;
    [SerializeField] private TextMeshProUGUI plateAmountText;
    [SerializeField] private Button nextPlateButton;
    [Header("Prefab")]
    [SerializeField] private Plate platePrefab;
    [Header("Dish Washing")]
    [SerializeField] private int gamePlateAmount = 5;

    // Plate
    private Plate previousPlate;
    private Plate currentPlate;
    // Dish washing
    private int currentPlateAmount = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(dishWashingAnimation, "dishWashingAnimation is missing");
        Debug.Assert(content, "content is missing");
        Debug.Assert(plateAmountText, "plateAmountText is missing");
        Debug.Assert(plateParent, "plateParent is missing");
        Debug.Assert(nextPlateButton, "nextPlateButton is missing");
        // Connect event
        dishWashingAnimation.OnScreenAppearFinished += (object sender, EventArgs e) =>
        {
            // Create and animate new plate
            currentPlate = SpawnPlate();
            dishWashingAnimation.DoPlateAppear(currentPlate);
        };
        dishWashingAnimation.OnScreenDisappearFinished += (object sender, EventArgs e) =>
        {
            content.gameObject.SetActive(false);
            OnMinigameEnded.Invoke(this, EventArgs.Empty);
        };
        dishWashingAnimation.OnPlateAppearFinished += (object sender, EventArgs e) =>
        {
            // Show next plate button
            nextPlateButton.gameObject.SetActive(true);
            // Set sponge active
            Sponge.Instance.Active = true;
        };
        dishWashingAnimation.OnPlateDisappearFinished += (object sender, EventArgs e) =>
        {
            // Destroy previous plate
            if (previousPlate)
            {
                Destroy(previousPlate.gameObject);
                previousPlate = null;
            }
            // Check is game finished
            if (currentPlateAmount >= gamePlateAmount) EndMinigame();
        };
        // Initialize
        content.gameObject.SetActive(false);
    }
    #endregion

    // ====================================================================================================
    //                     Minigame Functions
    // ====================================================================================================
    #region Minigame
    public void StartMinigame()
    {
        // Set active
        content.gameObject.SetActive(true);
        // Setup minigame
        previousPlate = null;
        currentPlate = null;
        currentPlateAmount = 0;
        UpdatePlateAmount();
        Sponge.Instance.Active = false;
        nextPlateButton.gameObject.SetActive(false);
        // Start sequence
        dishWashingAnimation.DoScreenAppear();
    }

    public void EndMinigame()
    {
        Sponge.Instance.Active = false;
        dishWashingAnimation.DoScreenDisappear();
    }
    #endregion

    // ====================================================================================================
    //                     Dish Washing Functions
    // ====================================================================================================
    #region Dish Washing
    public void NextPlate()
    {
        // Set sponge not active
        Sponge.Instance.Active = false;
        // Animate current plate to disappear
        if (currentPlate)
        {
            previousPlate = currentPlate;
            dishWashingAnimation.DoPlateDisappear(previousPlate);
        }
        // Update plate amount
        currentPlateAmount++;
        UpdatePlateAmount();
        // Create and animate new plate
        if (currentPlateAmount < gamePlateAmount)
        {
            currentPlate = SpawnPlate();
            dishWashingAnimation.DoPlateAppear(currentPlate);
        }
        // Hide next plate button
        nextPlateButton.gameObject.SetActive(false);
    }

    private Plate SpawnPlate()
    {
        // Create instance
        Plate plateInstance = Instantiate(platePrefab);
        // Set plate
        plateInstance.GeneratePlateDirt();
        // Set parent
        plateInstance.transform.SetParent(plateParent);
        // Return instance
        return plateInstance;
    }

    private void UpdatePlateAmount()
    {
        plateAmountText.text = 
            $"Plate Left : {(gamePlateAmount - currentPlateAmount).ToString()}" +
            "\n" +
            $"Plate Completed : {currentPlateAmount.ToString()}"
        ;
    }
    #endregion
}
