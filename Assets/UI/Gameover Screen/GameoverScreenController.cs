using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverScreenController : MonoBehaviour
{
    [Header("Component and Object")]
    // Game object
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject winScreenContent;
    [SerializeField] private GameObject loseScreenContent;
    // Component
    [SerializeField] private CanvasGroup contentCanvasGroup;
    [SerializeField] private TextMeshProUGUI titleText;
    [Header("Scene")]
    [SerializeField] private string restartTargerSceneName = "GameScene";
    [SerializeField] private string backToMenuTargetSceneName = "MainMenuScene";
    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 1.0f;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(content, "content is missing");
        Debug.Assert(winScreenContent, "winScreenContent is missing");
        Debug.Assert(loseScreenContent, "loseScreenContent is missing");
        Debug.Assert(contentCanvasGroup, "contentCanvasGroup is missing");
        Debug.Assert(titleText, "titleText is missing");
        // Initialize
        content.SetActive(false);
        winScreenContent.SetActive(false);
        loseScreenContent.SetActive(false);
        contentCanvasGroup.alpha = 0.0f;
    }
    #endregion

    // ====================================================================================================
    //                     Gameover Functions
    // ====================================================================================================
    #region Gameover
    public void OpenWinScreen()
    {
        // Set win screen
        content.SetActive(true);
        winScreenContent.SetActive(true);
        titleText.text = "You Win!";
        // Do fade in
        DOTween.To(
            () => {return contentCanvasGroup.alpha;},
            (float value) => {contentCanvasGroup.alpha = value;},
            1.0f,
            fadeInDuration
        );
    }

    public void OpenLoseScreen()
    {
        // Set lose screen
        content.gameObject.SetActive(true);
        loseScreenContent.SetActive(true);
        titleText.text = "You Lose!";
        // Do fade in
        DOTween.To(
            () => {return contentCanvasGroup.alpha;},
            (float value) => {contentCanvasGroup.alpha = value;},
            1.0f,
            fadeInDuration
        );
    }
    #endregion

    // ====================================================================================================
    //                     Button Functions
    // ====================================================================================================
    #region Button
    public void OnRestart()
    {
        SceneManager.LoadScene(restartTargerSceneName);
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene(backToMenuTargetSceneName);
    }
    #endregion
}
