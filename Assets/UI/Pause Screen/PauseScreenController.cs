using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private GameObject content;
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
        // Initialize
        content.SetActive(false);
    }
    #endregion

    // ====================================================================================================
    //                     Pause Functions
    // ====================================================================================================
    #region Pause
    public void OpenPauseScreen()
    {
        Time.timeScale = 0.0f;
        content.SetActive(true);
    }

    public void ClosePauseScreen()
    {
        Time.timeScale = 1.0f;
        content.SetActive(false);
    }

    public void OnRestart()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(restartTargerSceneName);
    }

    public void OnBackToMenu()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(backToMenuTargetSceneName);
    }

    public bool isGamePaused() {return Time.timeScale == 0.0f;}
    #endregion
}
