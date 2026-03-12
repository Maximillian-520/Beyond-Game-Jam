using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DishWashingAnimation : MonoBehaviour
{
    public event EventHandler OnScreenAppearFinished;
    public event EventHandler OnScreenDisappearFinished;
    public event EventHandler OnPlateAppearFinished;
    public event EventHandler OnPlateDisappearFinished;
    public event EventHandler OnDishWashingResultFinished;

    [Header("Component and Object")]
    [SerializeField] private CanvasGroup screenCanvasGroup;
    [SerializeField] private RectTransform plateCenterPosition;
    [SerializeField] private RectTransform plateAppearPosition;
    [SerializeField] private RectTransform plateDisappearPosition;
    [SerializeField] private TextMeshProUGUI dishWashingResultText;
    [Header("Animation")]
    [SerializeField] private float screenFadeDuration = 1.0f;
    [SerializeField] private float plateMoveDuration = 1.0f;
    [SerializeField] private float resultShowDuration = 2.5f;
    [SerializeField] private float resultTransistionDuration = 0.5f;

    // Tween
    private Tween screenAppearTween;
    private Tween screenDisappearTween;
    private Tween plateAppearTween;
    private Tween plateDisappearTween;
    private Tween dishWashingResultTween;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(screenCanvasGroup, "screenCanvasGroup is missing");
        Debug.Assert(plateCenterPosition, "plateCenterPosition is missing");
        Debug.Assert(plateAppearPosition, "plateAppearPosition is missing");
        Debug.Assert(plateDisappearPosition, "plateDisappearPosition is missing");
        Debug.Assert(dishWashingResultText, "dishWashingResultText is missing");
    }
    #endregion

    // ====================================================================================================
    //                     Animation Functions
    // ====================================================================================================
    #region Animation
    public void DoScreenAppear()
    {
        // Check if tween exist
        if (!screenAppearTween.IsUnityNull()) screenAppearTween.Kill();
        // Do animation
        screenCanvasGroup.alpha = 0.0f;
        screenAppearTween = DOTween.To(
            ()=>screenCanvasGroup.alpha,
            x => screenCanvasGroup.alpha = x,
            1.0f,
            screenFadeDuration
        );
        // Connect event
        screenAppearTween.OnComplete(() =>
        {
            screenAppearTween = null;
            OnScreenAppearFinished.Invoke(this, EventArgs.Empty);
        });
    }

    public void DoScreenDisappear()
    {
        // Check if tween exist
        if (!screenDisappearTween.IsUnityNull()) screenDisappearTween.Kill();
        // Do animation
        screenCanvasGroup.alpha = 1.0f;
        screenDisappearTween = DOTween.To(
            ()=>screenCanvasGroup.alpha,
            x => screenCanvasGroup.alpha = x,
            0.0f,
            screenFadeDuration
        );
        // Connect event
        screenDisappearTween.OnComplete(() =>
        {
            screenDisappearTween = null;
            OnScreenDisappearFinished.Invoke(this, EventArgs.Empty);
        });
    }

    public void DoPlateAppear(Plate plate)
    {
        // Check if tween exist
        if (!plateAppearTween.IsUnityNull()) plateAppearTween.Kill();
        // Do animation
        plate.transform.position = plateAppearPosition.position;
        plateAppearTween = plate.transform.DOMove(
            plateCenterPosition.position,
            plateMoveDuration
        );
        // Connect event
        plateAppearTween.OnComplete(() =>
        {
            plateAppearTween = null;
            OnPlateAppearFinished.Invoke(this, EventArgs.Empty);
        });
    }

    public void DoPlateDisappear(Plate plate)
    {
        // Check if tween exist
        if (!plateDisappearTween.IsUnityNull()) plateDisappearTween.Kill();
        // Do animation
        plate.transform.position = plateCenterPosition.position;
        plateDisappearTween = plate.transform.DOMove(
            plateDisappearPosition.position,
            plateMoveDuration
        );
        // Connect event
        plateDisappearTween.OnComplete(() =>
        {
            plateDisappearTween = null;
            OnPlateDisappearFinished.Invoke(this, EventArgs.Empty);
        });
    }

    public IEnumerator DoDishWashingResult()
    {
        // Transistion
        dishWashingResultText.gameObject.SetActive(false);
        yield return new WaitForSeconds(resultTransistionDuration);
        // Show
        dishWashingResultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(resultShowDuration);
        // Transistion
        dishWashingResultText.gameObject.SetActive(false);
        yield return new WaitForSeconds(resultTransistionDuration);
        // Finished
        OnDishWashingResultFinished.Invoke(this, EventArgs.Empty);
        
        // dishWashingResultTween = ;
    }
    #endregion
}
