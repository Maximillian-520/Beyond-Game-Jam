using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverScreenController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject winScreenContent;
    [SerializeField] private GameObject loseScreenContent;
    [SerializeField] private TextMeshProUGUI titleText;
    [Header("Scene")]
    [SerializeField] private string restartTargerSceneName = "GameScene";
    [SerializeField] private string backToMenuTargetSceneName = "MainMenuScene";

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
        Debug.Assert(titleText, "titleText is missing");
        // Initialize
        content.SetActive(false);
        winScreenContent.SetActive(false);
        loseScreenContent.SetActive(false);
    }
    #endregion

    // ====================================================================================================
    //                     Gameover Functions
    // ====================================================================================================
    #region Gameover
    public void OpenWinScreen()
    {
        content.SetActive(true);
        winScreenContent.SetActive(true);
        titleText.text = "You Win!";
    }

    public void OpenLoseScreen()
    {
        content.gameObject.SetActive(true);
        loseScreenContent.SetActive(true);
        titleText.text = "You Lose!";
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
