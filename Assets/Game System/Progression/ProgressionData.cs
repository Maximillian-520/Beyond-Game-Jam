using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProgressionData")]
public class ProgressionData : ScriptableObject
{
    [Tooltip("How long will the game last (minus the time for chores minigames)")]
    public float gameTime;
    [Tooltip("Minimum time before chore break")]
    public float minChoresTime;
    [Tooltip("Maximum time before chore break")]
    public float maxChoresTime;
    [Tooltip(
        "Shows the spawn data for each level.\n" +
        "Game spawn data level get incremented after finishing a chore minigame."
    )]
    public List<SpawnData> spawnDataList;
}

[Serializable]
public struct SpawnData
{
    public float minSpawnTime;
    public float maxSpawnTime;
}
