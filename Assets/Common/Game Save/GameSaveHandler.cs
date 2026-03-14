using System.IO;
using UnityEngine;

public class GameSaveHandler : MonoBehaviour
{
    const string GAME_DATA_FILE_NAME = "/GameData.save"; // Change as needed

    static public void SaveGameData(GameData gameData)
    {
        File.WriteAllText(GetGameDataFilePath(), JsonUtility.ToJson(gameData));
        Debug.Log("Game data saved");
    }

    static public GameData LoadGameData()
    {
        // Check is file exist
        if (!File.Exists(GetGameDataFilePath()))
        {
            // Return empty save data
            Debug.Log("Game data did not exists");
            return new GameData{isEmpty = true};;
        }
        // Read and return save data
        else
        {
            string gameDataText = File.ReadAllText(GetGameDataFilePath());
            GameData gameData = JsonUtility.FromJson<GameData>(gameDataText);
            Debug.Log("Game data loaded");
            return gameData;
        }
    }

    static private string GetGameDataFilePath()
    {
        string filePath = Application.persistentDataPath + GAME_DATA_FILE_NAME;
        return filePath;
    }

}

[System.Serializable]
public struct GameData
{
    // Write your own save data struct
    public bool isEmpty;
    public bool isGameDefeated;
}
