using System;
using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    public enum DifficultyName
    {
        EASY,
        NORMAL,
        HARD,
    }

    public static DifficultyName currentDifficulty;

    [Header("Component and Object")]
    [SerializeField] private AttackObjectSpawner attackObjectSpawner;
    [SerializeField] private GameTimer gameTimer;
    [Header("Progression")]
    [SerializeField] private ProgressionData easyDifficultyData;
    [SerializeField] private ProgressionData normalDifficultyData;
    [SerializeField] private ProgressionData hardDifficultyData;

    private ProgressionData currentProgressionData;

    int currentProgressionLevel = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(attackObjectSpawner, "attackObjectSpawner is missing");
        Debug.Assert(gameTimer, "gameTimer is missing");
        Debug.Assert(easyDifficultyData, "easyDifficultyData is empty");
        Debug.Assert(
            easyDifficultyData.spawnDataList.Count > 0, "easyDifficultyData spawnDataList is missing"
        );
        Debug.Assert(normalDifficultyData, "normalDifficultyData is empty");
        Debug.Assert(
            normalDifficultyData.spawnDataList.Count > 0, "normalDifficultyData spawnDataList is missing"
        );
        Debug.Assert(hardDifficultyData, "hardDifficultyData is empty");
        Debug.Assert(
            hardDifficultyData.spawnDataList.Count > 0, "hardDifficultyData spawnDataList is missing"
        );
        // Set progression data
        switch (currentDifficulty)
        {
            case DifficultyName.EASY:{currentProgressionData = easyDifficultyData; break;}
            case DifficultyName.NORMAL:{currentProgressionData = normalDifficultyData; break;}
            case DifficultyName.HARD:{currentProgressionData = hardDifficultyData; break;}
        }
        // Initialize
        SetSpawnData(currentProgressionData.spawnDataList[0]);
        gameTimer.SetGameTime(
            currentProgressionData.gameTime,
            currentProgressionData.minChoresTime,
            currentProgressionData.maxChoresTime
        );
    }
    #endregion

    // ====================================================================================================
    //                     Progression Functions
    // ====================================================================================================
    #region Progression
    public void NextProgressionLevel()
    {
        // Increment level
        currentProgressionLevel++;
        // Get progression data
        int listIndex = Math.Min(currentProgressionLevel, currentProgressionData.spawnDataList.Count - 1);
        SpawnData spawnData = currentProgressionData.spawnDataList[listIndex];
        // Set progression data
        SetSpawnData(spawnData);
    }

    private void SetSpawnData(SpawnData spawnData)
    {
        attackObjectSpawner.minSpawnTime = spawnData.minSpawnTime;
        attackObjectSpawner.maxSpawnTime = spawnData.maxSpawnTime;
    }
    #endregion
}
