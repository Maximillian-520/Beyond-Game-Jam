using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {private set; get;}

    [Header("Component and Object")]
    // Game Object
    [SerializeField] private Player player;
    [SerializeField] private AttackObjectSpawner attackObjectSpawner;
    [SerializeField] private UIAnimationController uiAnimationController;
    [SerializeField] private DishWashingController dishWashingController;
    [SerializeField] private GameoverScreenController gameoverScreenController;
    [SerializeField] private PauseScreenController pauseScreenController;
    // System
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private ProgressionController progressionController;
    [Header("Music")]
    [SerializeField] private string survivalMusicName = "Survival";
    [SerializeField] private float musicFadeDuration = 0.8f;
    [Header("Debug")]
    public DebugData debugData;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Awake() {Instance = this;}

    private void OnDestroy() {Instance = null;}

    private void Start()
    {
        // Assertion check
        Debug.Assert(player, "player is missing");
        Debug.Assert(attackObjectSpawner, "attackObjectSpawner");
        Debug.Assert(uiAnimationController, "uiAnimationController is missing");
        Debug.Assert(dishWashingController, "dishWashingController is missing");
        Debug.Assert(gameoverScreenController, "gameoverScreenController is missing");
        Debug.Assert(pauseScreenController, "pauseScreenController is missing");
        Debug.Assert(gameTimer, "gameTimer is missing");
        Debug.Assert(progressionController, "progressionController is missing");
        Debug.Assert(debugData, "debugData is empty");
        // Connect events
        player.OnPlayerDied += (object sender, EventArgs e) => {EndGame(false);};
        uiAnimationController.OnOpeningSequenceFinished += (object sender, EventArgs e) =>
        {
            gameTimer.StartGameTimer();
            attackObjectSpawner.ResumeSpawnTimer();
            AudioManager.Instance.PlayMusic(survivalMusicName);
        };
        uiAnimationController.OnChoreBreakFinished += (object sender, EventArgs e) =>
        {
            dishWashingController.StartMinigame();
        };
        uiAnimationController.OnPlayerWinFinihsed += (object sender, EventArgs e) =>
        {
            gameoverScreenController.OpenWinScreen();
        };
        uiAnimationController.OnPlayerLoseFinihsed += (object sender, EventArgs e) =>
        {
            gameoverScreenController.OpenLoseScreen();
        };
        dishWashingController.OnMinigameEnded += (object sender, EventArgs e) =>
        {
            SwitchToSurvivalPhase();
        };
        gameTimer.OnGameTimerFinished += (object sender, EventArgs e) => {EndGame(true);};
        gameTimer.OnChoresTimerFired += (object sender, EventArgs e) => {SwitchToChoresPhase();};
        // Initialize
        #if !UNITY_EDITOR
        debugData = new DebugData();
        #endif
        if (!debugData.gameStartOnAwake) return;
        uiAnimationController.DoOpeningSequence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreenController.isGamePaused()) pauseScreenController.ClosePauseScreen();
            else pauseScreenController.OpenPauseScreen();
        }
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
        AudioManager.Instance.PlayMusic(survivalMusicName);
    }

    private void SwitchToChoresPhase()
    {
        // Stop survival
        player.Active = false;
        gameTimer.PauseGameTimer();
        attackObjectSpawner.PauseSpawnTimer();
        AudioManager.Instance.StopMusic(musicFadeDuration);
        // Play animation
        uiAnimationController.DoChoreBreak();
    }

    private void EndGame(bool isPlayerWin)
    {
        // Stop survival
        player.Active = false;
        gameTimer.PauseGameTimer();
        attackObjectSpawner.PauseSpawnTimer();
        // Open gameover screen
        if (isPlayerWin) uiAnimationController.DoPlayerWin();
        else uiAnimationController.DoPlayerLose();
        // Save data
        if (isPlayerWin)
        {
            GameSaveHandler.SaveGameData(new GameData{isEmpty = false, isGameDefeated = true});
        }
    }
    #endregion
}
