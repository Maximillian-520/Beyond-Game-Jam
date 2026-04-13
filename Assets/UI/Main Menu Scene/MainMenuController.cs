using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private GameObject mainContent;
    [SerializeField] private GameObject difficultyContent;
    [SerializeField] private GameObject settingsContent;
    [SerializeField] private GameObject creditsContent;
    [Header("Main Menu")]
    [SerializeField] private string playTargerSceneName = "GameScene";
    [SerializeField] private string mainMenuMusicName = "MainMenu";

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(mainContent, "mainContent is missing");
        Debug.Assert(difficultyContent, "difficultyContent is missing");
        Debug.Assert(settingsContent, "settingsContent is missing");
        Debug.Assert(creditsContent, "creditsContent is missing");
        // Initialize
        mainContent.SetActive(true);
        difficultyContent.SetActive(false);
        settingsContent.SetActive(false);
        creditsContent.SetActive(false);
        AudioManager.Instance.PlayMusic(mainMenuMusicName);
    }
    #endregion

    // ====================================================================================================
    //                     Button Functions
    // ====================================================================================================
    #region Button
    public void OnPlay()
    {
        mainContent.SetActive(false);
        difficultyContent.SetActive(true);
        settingsContent.SetActive(false);
        creditsContent.SetActive(false);
    }

    public void OnEasyDifficultySelected()
    {
        ProgressionController.currentDifficulty = ProgressionController.DifficultyName.EASY;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(playTargerSceneName);
    }

    public void OnNormalDifficultySelected()
    {
        ProgressionController.currentDifficulty = ProgressionController.DifficultyName.NORMAL;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(playTargerSceneName);
    }

    public void OnHardDifficultySelected()
    {
        ProgressionController.currentDifficulty = ProgressionController.DifficultyName.HARD;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(playTargerSceneName);
    }

    public void OnSettings()
    {
        mainContent.SetActive(false);
        difficultyContent.SetActive(false);
        settingsContent.SetActive(true);
        creditsContent.SetActive(false);
    }

    public void OnCredits()
    {
        mainContent.SetActive(false);
        difficultyContent.SetActive(false);
        settingsContent.SetActive(false);
        creditsContent.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnBack()
    {
        mainContent.SetActive(true);
        difficultyContent.SetActive(false);
        settingsContent.SetActive(false);
        creditsContent.SetActive(false);
    }
    #endregion
}
