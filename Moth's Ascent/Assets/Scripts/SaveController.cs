using UnityEngine;

/// <summary>
/// Handles saving and loading the game state, including the player's position and the current map boundary. 
/// The save data is stored in a JSON file in the persistent data path.
/// If no save file exists when loading, a new one will be created with the current state.
/// </summary>
public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the save location to a file in the persistent data path
        saveLocation = Application.persistentDataPath + "/savefile.json";
    }

    void SaveGame()
    {
        // Save the player's position and the current map boundary to a SaveData object
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name
        };

        // Write to the JSON save file
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if(File.Exists(saveLocation))
        {
            // Read the JSON save file and deserialize it into a SaveData object
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            // Set the player's position to the saved position
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
        }
        else
        {
            // If no save file exists, create one with the current state
            SaveGame();
            Debug.Log("No save file found. A new save file has been created.");
        }
    }
}
