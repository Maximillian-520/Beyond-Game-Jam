using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private AttackObjectSpawner attackObjectSpawner;
    [Header("Progression")]
    [SerializeField] private List<ProgressionData> progressionDataList;

    int currentProgressionLevel = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(attackObjectSpawner, "attackObjectSpawner is missing");
        Debug.Assert(progressionDataList.Count > 0, "progressionDataList is missing");
        // Initialize
        SetProgressionData(progressionDataList[0]);
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
        int listIndex = Math.Min(currentProgressionLevel, progressionDataList.Count - 1);
        ProgressionData progressionData = progressionDataList[listIndex];
        // Set progression data
        SetProgressionData(progressionData);
    }

    private void SetProgressionData(ProgressionData progressionLevelData)
    {
        attackObjectSpawner.minSpawnTime = progressionLevelData.minSpawnTime;
        attackObjectSpawner.maxSpawnTime = progressionLevelData.maxSpawnTime;
    }
    #endregion
}

[Serializable]
public struct ProgressionData
{
    public float minSpawnTime;
    public float maxSpawnTime;
}
