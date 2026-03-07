using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles saving and loading the game state, including the player's position and the current map boundary. 
/// The save data is stored in a JSON file in the persistent data path.
/// If no save file exists when loading, a new one will be created with the current state.
/// </summary>
public class SaveController : MonoBehaviour
{   
    [SerializeField] private int totalSlots = 0;
    // Assign the player data in inspector
    [SerializeField] private PlayerData playerData = 0;
    // Singleton instance of the SaveController
    public static SaveController Instance { get; private set; }
    // The time when the current session started, used for tracking playtime
    private float _sessionStartTime;

    /// <summary>
    /// Ensures that the SaveController is a singleton instance and persists across scenes. 
    /// If another instance is created, it will be destroyed.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the timer for tracking playtime
        _sessionStartTime = Time.time;
    }

    public void SaveGame()
    {   
        // Accumulate playtime and reset so next save won't double count time between saves
        float sessionPlayTime = Time.time - _sessionStartTime;
        _sessionStartTime = Time.time;

        // Load existing data to preserve playtime across saves
        SaveData existing = ReadSaveSlot(saveSlot);
        // If existing data exists, add the session playtime to the total playtime, otherwise start at 0
        float previousPlayTime = existing != null ? existing.totalPlayTimeSeconds : 0f;

        // Create a new SaveData object to hold essentials
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            currentHP = playerData.currentHP,
            inventoryItems = InventoryManager.Instace != null 
                ? new List<string>(InventoryManager.Instance.Items)
                : new List<string>(),
            clearedEncountersFlags = ProgressTracker.Instance != null 
                ? new List<string>(ProgressTracker.Instance.StoryProgressionFlags) 
                : new List<string>(),
            saveTimestap = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            totalPlayTimeSeconds = previousPlayTime + sessionPlayTime,
            locationName = locationName
        };

        // Write to the JSON save file
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game successfullysaved to slot " + saveSlot);
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

    // Helper property that returns the file path for a given slot index
    private string SlotPath(int slot) => Application.persistentDataPath + "/savefile_" + slot + ".json";
}
