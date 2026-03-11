using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string playTargerSceneName = "GameScene";

    // ====================================================================================================
    //                     Button Functions
    // ====================================================================================================
    #region Button
    public void OnPlay()
    {
        SceneManager.LoadScene(playTargerSceneName);
    }

    public void OnSettings()
    {
        
    }

    public void OnCredits()
    {
        
    }

    public void OnQuit()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }
    #endregion
}
