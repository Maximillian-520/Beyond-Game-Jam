using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DishWashingController dishWashingController;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameoverScreenController gameoverScreenController;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(player, "player is missing");
        Debug.Assert(dishWashingController, "dishWashingController is missing");
        Debug.Assert(gameTimer, "gameTimer is missing");
        Debug.Assert(gameoverScreenController, "gameoverScreenController is missing");
        // Connect events
        player.OnPlayerDied += (object sender, EventArgs e) => {EndGame(false);};
        dishWashingController.OnMinigameEnded += (object sender, EventArgs e) => {
            SwitchToSurvivalPhase();
        };
        gameTimer.OnGameTimerFinished += (object sender, EventArgs e) => {EndGame(true);};
        gameTimer.OnChoresTimerFired += (object sender, EventArgs e) => {SwitchToChoresPhase();};
        // Initialize
        gameTimer.StartGameTimer();
    }
    #endregion

    // ====================================================================================================
    //                     Flow Functions
    // ====================================================================================================
    #region Flow
    private void SwitchToSurvivalPhase()
    {
        player.Active = true;
        gameTimer.ResumeGameTimer();
    }

    private void SwitchToChoresPhase()
    {
        player.Active = false;
        gameTimer.PauseGameTimer();
        dishWashingController.StartMinigame();
    }

    private void EndGame(bool isPlayerWin)
    {
        player.Active = false;
        gameTimer.EndGameTimer();
        if (isPlayerWin) gameoverScreenController.OpenWinScreen();
        else gameoverScreenController.OpenLoseScreen();
    }
    #endregion
}
