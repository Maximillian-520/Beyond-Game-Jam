using TMPro;
using UnityEngine;

public class DishWashingResult : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private TextMeshProUGUI resultTitleText;
    [SerializeField] private TextMeshProUGUI resultRemainingDirtText;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [Header("Result")]
    // [Tooltip("Actual threshold = resultBadThreshold - resultHorribleThreshold ")]
    // [SerializeField] private float resultHorribleThreshold = 1.0f;
    [Tooltip("Actual threshold = resultNormalThreshold - resultBadThreshold")]
    [SerializeField] private float resultBadThreshold = 0.8f;
    [Tooltip("Actual threshold = resultGoodThreshold - resultNormalThreshold")]
    [SerializeField] private float resultNormalThreshold = 0.6f;
    [Tooltip("Actual threshold = resultGreatThreshold - resultGoodThreshold")]
    [SerializeField] private float resultGoodThreshold = 0.4f;
    [Tooltip("Actual threshold = 0 - resultGreatThreshold")]
    [SerializeField] private float resultGreatThreshold = 0.2f;
    [SerializeField] private Color resultHorribleColor = Color.white;
    [SerializeField] private Color resultBadColor = Color.white;
    [SerializeField] private Color resultNormalColor = Color.white;
    [SerializeField] private Color resultGoodColor = Color.white;
    [SerializeField] private Color resultGreatColor = Color.white;
    [Header("Speed Effect")]
    [SerializeField] private float speedEffectDuration = 10f;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(resultTitleText, "resultTitleText is missing");
        Debug.Assert(resultRemainingDirtText, "resultRemainingDirtText is missing");
        Debug.Assert(resultScoreText, "resultScoreText is missing");
    }
    #endregion

    // ====================================================================================================
    //                     Result Functions
    // ====================================================================================================
    #region Result
    public void SetResultText(float remainingResult)
    {
        // Set text
        resultRemainingDirtText.text = $"Remainding Dirt = {(int)(remainingResult * 100.0f)}%";
        resultScoreText.text = "<color=#89BFFC>Overall Score = </color>";
        resultScoreText.text += $"{GetResultScoreText(remainingResult)}";
        resultScoreText.color = GetResultScoreColor(remainingResult);
    }

    public void ApplySpeedEffect(float remainingResult)
    {
        // Great or Good
        if (remainingResult < resultGoodThreshold)
        {
            float valueNormalized = remainingResult / resultGoodThreshold;
            valueNormalized = 1.0f - valueNormalized;
            Player.Instance.playerBuffController.GiveSpeedBuff(valueNormalized, speedEffectDuration);
        }
        // Bad or Horrible
        else if (remainingResult > resultNormalThreshold)
        {
            float valueNormalized = remainingResult - resultNormalThreshold;
            valueNormalized /= 1.0f - resultNormalThreshold;
            Player.Instance.playerBuffController.GiveSpeedDebuff(valueNormalized, speedEffectDuration);
        }
    }

    private string GetResultScoreText(float remainingResult)
    {
        // Great
        if (remainingResult < resultGreatThreshold) return "Great";
        // Good
        else if (remainingResult < resultGoodThreshold) return "Good";
        // Normal
        else if (remainingResult < resultNormalThreshold) return "Meh";
        // Bad
        else if (remainingResult < resultBadThreshold) return "Bad";
        // Horrible
        else return "Horrible";
    }

    private Color GetResultScoreColor(float remainingResult)
    {
        // Great
        if (remainingResult < resultGreatThreshold) return resultGreatColor;
        // Good
        else if (remainingResult < resultGoodThreshold) return resultGoodColor;
        // Normal
        else if (remainingResult < resultNormalThreshold) return resultNormalColor;
        // Bad
        else if (remainingResult < resultBadThreshold) return resultBadColor;
        // Horrible
        else return resultHorribleColor;
    }
    #endregion
}
