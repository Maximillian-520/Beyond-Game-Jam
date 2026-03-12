using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Component and Object")]
    // Game Object
    [SerializeField] private Player player;
    [SerializeField] private AttackObjectSpawner attackObjectSpawner;
    [SerializeField] private DishWashingController dishWashingController;
    [SerializeField] private GameoverScreenController gameoverScreenController;
    // System
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private ProgressionController progressionController;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(player, "player is missing");
        Debug.Assert(attackObjectSpawner, "attackObjectSpawner");
        Debug.Assert(dishWashingController, "dishWashingController is missing");
        Debug.Assert(gameoverScreenController, "gameoverScreenController is missing");
        Debug.Assert(gameTimer, "gameTimer is missing");
        Debug.Assert(progressionController, "progressionController");
        // Connect events
        player.OnPlayerDied += (object sender, EventArgs e) => {EndGame(false);};
        dishWashingController.OnMinigameEnded += (object sender, EventArgs e) => {
            SwitchToSurvivalPhase();
        };
        gameTimer.OnGameTimerFinished += (object sender, EventArgs e) => {EndGame(true);};
        gameTimer.OnChoresTimerFired += (object sender, EventArgs e) => {SwitchToChoresPhase();};
        // Initialize
        gameTimer.StartGameTimer();
        attackObjectSpawner.ResumeSpawnTimer();
    }
    #endregion

    // ====================================================================================================
    //                     Flow Functions
    // ====================================================================================================
    #region Flow
    private void SwitchToSurvivalPhase()
    {
        // Update progression
        progressionController.NextProgressionLevel();
        // Start survival
        player.Active = true;
        gameTimer.ResumeGameTimer();
        attackObjectSpawner.ResumeSpawnTimer();
    }

    private void SwitchToChoresPhase()
    {
        // Stop survival
        player.Active = false;
        gameTimer.PauseGameTimer();
        attackObjectSpawner.PauseSpawnTimer();
        // Start minigame
        dishWashingController.StartMinigame();
    }

    private void EndGame(bool isPlayerWin)
    {
        // Stop survival
        player.Active = false;
        gameTimer.PauseGameTimer();
        attackObjectSpawner.PauseSpawnTimer();
        // Open gameover screen
        if (isPlayerWin) gameoverScreenController.OpenWinScreen();
        else gameoverScreenController.OpenLoseScreen();
    }
    #endregion
}
