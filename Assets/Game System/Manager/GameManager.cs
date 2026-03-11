using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private Player player;
    // [SerializeField] private Enemy enemy;
    [SerializeField] private DishWashingController dishWashingController;
    [SerializeField] private GameTimer gameTimer;
    // [SerializeField] private GameoverScreenController gameoverScreenController;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        // Debug.Assert(player, "player is missing");
        // Debug.Assert(enemy, "enemy is missing");
        Debug.Assert(dishWashingController, "dishWashingController is missing");
        Debug.Assert(gameTimer, "gameTimer is missing");
        // Debug.Assert(gameoverScreenController, "gameoverScreenController is missing");
        // Connect events
        // player.OnPlayerDied += (object sender, EventArgs e) => {EndGame(false);};
        // enemy.OnEnemyDied += (object sender, EventArgs e) => {EndGame(true);};
        // dishWashingController.OnMinigameEnded += (object sender, EventArgs e) => {StartBattlePhase();};
        // gameTimer.OnBattleTimerFinished += (object sender, EventArgs e) => {StartChoresPhase();};
        // Initialize
        // StartBattlePhase();
    }
    #endregion

    // ====================================================================================================
    //                     Flow Functions
    // ====================================================================================================
    #region Flow
    // private void StartBattlePhase()
    // {
    //     player.Active = true;
    //     enemy.Active = true;
    //     gameTimer.StartBattleTimer();
    // }

    // private void StartChoresPhase()
    // {
    //     player.Active = false;
    //     enemy.Active = false;
    //     dishWashingController.StartMinigame();
    // }

    // private void EndGame(bool isPlayerWin)
    // {
    //     player.Active = false;
    //     enemy.Active = false;
    //     gameTimer.EndBattleTimer();
    //     if (isPlayerWin) gameoverScreenController.OpenWinScreen();
    //     else gameoverScreenController.OpenLoseScreen();
    // }
    #endregion
}
