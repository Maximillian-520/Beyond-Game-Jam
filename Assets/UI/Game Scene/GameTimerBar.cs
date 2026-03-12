using UnityEngine;
using UnityEngine.UI;

public class GameTimerBar : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private Image barFill;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(gameTimer, "gameTimer is missing");
        Debug.Assert(barFill, "barFill is missing");
    }

    private void Update()
    {
        // Update bar
        barFill.fillAmount = gameTimer.GetGameTimerNormalized();
    }
    #endregion
}
