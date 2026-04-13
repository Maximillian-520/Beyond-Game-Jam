using UnityEngine;

[CreateAssetMenu(fileName = "DebugData")]
public class DebugData : ScriptableObject
{
    [Tooltip("Immune to any damage, default is false")]
    public bool playerImmune = false;
    [Tooltip("Start the game or not")]
    public bool gameStartOnAwake = true;
}
