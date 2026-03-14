using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DishWashingController : MonoBehaviour
{
    public event EventHandler OnMinigameEnded;

    [Header("Component and Object")]
    [SerializeField] private DishWashingAnimation dishWashingAnimation;
    [SerializeField] private DishWashingResult dishWashingResult;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject resultContent;
    [SerializeField] private RectTransform plateParent;
    [SerializeField] private TextMeshProUGUI plateAmountText;
    [SerializeField] private Button nextPlateButton;
    [Header("Prefab")]
    [SerializeField] private Plate platePrefab;
    [Header("Dish Washing")]
    [SerializeField] private int gamePlateAmount = 5;
    [Header("Music")]
    [SerializeField] private string choresMusicName = "Chores";
    [SerializeField] private float musicFadeDuration = 0.8f;

    // Plate
    private Plate previousPlate;
    private Plate currentPlate;
    // Dish washing
    private int currentPlateAmount = 0;
    private List<float> remainingDirtThicknessList = new List<float>();

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(dishWashingAnimation, "dishWashingAnimation is missing");
        Debug.Assert(dishWashingResult, "dishWashingResult is missing");
        Debug.Assert(content, "content is missing");
        Debug.Assert(resultContent, "resultContent is missing");
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
            content.SetActive(false);
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
            // Get remaining dirt thickness
            remainingDirtThicknessList.Add(previousPlate.GetRemainingDirtThickness());
            // Destroy previous plate
            if (previousPlate)
            {
                Destroy(previousPlate.gameObject);
                previousPlate = null;
            }
            // Check is no plates left
            if (currentPlateAmount >= gamePlateAmount) ShowResult();
        };
        dishWashingAnimation.OnDishWashingResultFinished += (object sender, EventArgs e) =>
        {
            // End minigame
            EndMinigame();
            // Apply speed effect
            float remainingResult = remainingDirtThicknessList.Sum() / remainingDirtThicknessList.Count;
            dishWashingResult.ApplySpeedEffect(remainingResult);
        };
        // Initialize
        plateAmountText.gameObject.SetActive(false);
        resultContent.SetActive(false);
        content.SetActive(false);
    }
    #endregion

    // ====================================================================================================
    //                     Minigame Functions
    // ====================================================================================================
    #region Minigame
    public void StartMinigame()
    {
        // Set active
        content.SetActive(true);
        plateAmountText.gameObject.SetActive(true);
        // Setup minigame
        previousPlate = null;
        currentPlate = null;
        currentPlateAmount = 0;
        UpdatePlateAmount();
        remainingDirtThicknessList.Clear();
        Sponge.Instance.Active = false;
        nextPlateButton.gameObject.SetActive(false);
        // Start music
        AudioManager.Instance.PlayMusic(choresMusicName);
        // Start sequence
        dishWashingAnimation.DoScreenAppear();
    }

    public void EndMinigame()
    {
        // Clear minigame
        Sponge.Instance.Active = false;
        dishWashingAnimation.DoScreenDisappear();
        // Start music
        AudioManager.Instance.StopMusic(musicFadeDuration);
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
            $"Plate Left : {gamePlateAmount - currentPlateAmount}" +
            "\n" +
            $"Plate Completed : {currentPlateAmount}"
        ;
    }
    #endregion

    // ====================================================================================================
    //                     Result Functions
    // ====================================================================================================
    #region Result
    private void ShowResult()
    {
        // Set active
        plateAmountText.gameObject.SetActive(false);
        resultContent.SetActive(true);
        // Calculate and set results
        float remainingResult = remainingDirtThicknessList.Sum() / remainingDirtThicknessList.Count;
        dishWashingResult.SetResultText(remainingResult);
        // Do animation
        StartCoroutine(dishWashingAnimation.DoDishWashingResult());
    }
    #endregion
}
