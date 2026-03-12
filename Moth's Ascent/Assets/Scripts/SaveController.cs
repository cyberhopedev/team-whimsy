using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles saving and loading the game state, including the player's position and the current map boundary. 
/// The save data is stored in a JSON file in the persistent data path.
/// If no save file exists when loading, a new one will be created with the current state.
/// </summary>
public class SaveController : MonoBehaviour
{   
    // Total amount of save slots available, can be adjusted in inspector
    [SerializeField] private int totalSlots = 3;
    // Assign the player data in inspector
    [SerializeField] private PlayerData playerData;
    // Singleton instance of the SaveController
    public static SaveController Instance { get; private set; }
    // The time when the current session started, used for tracking playtime
    private float _sessionStartTime;
    // For when player doesn't exist yet (load screen)
    private Vector3 _pendingSpawnPosition;
    // Tracks which game is being played atm
    int currentSlotIdx;

    /// <summary>
    /// Ensures that the SaveController is a singleton instance and persists across scenes. 
    /// If another instance is created, it will be destroyed.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the timer for tracking playtime
        _sessionStartTime = Time.time;
    }

    public void NewGame(int slot, string gameName)
    {
        // Reset player data to defaults
        Debug.Log("playerData is: " + playerData); // if this prints null, it's the inspector
        playerData.ResetHP();
        playerData.knownAbilities = new List<Ability>() { Ability.STRUGGLE }; // reset to default
        
        SaveData saveData = new SaveData
        {
            playerPosition = Vector3.zero, 
            mapBoundary = "",
            currentHP = playerData.currentHP,
            inventoryItems = new List<ItemData>(),
            clearedEncountersFlags = new List<string>(),
            storyProgressionFlags = new List<string>(),
            saveTimestamp = DateTime.Now.ToString("yyyy-MM-dd\nHH:mm"),
            totalPlayTimeSeconds = 0f,
            playTimeString = "00:00:00",
            gameName = gameName,
            locationName = "Room 1",
            sceneName = "BetaScene"
        };

        File.WriteAllText(SlotPath(slot), JsonUtility.ToJson(saveData));
        _sessionStartTime = Time.time;
        currentSlotIdx = slot;
        Debug.Log("New game created in slot " + slot);
    }

    // Helper method for starting a new game
    public int GetFirstEmptySlot()
{
    for (int i = 0; i < totalSlots; i++)
    {
        if (!SaveSlotExists(i)) return i;
    }
    return -1; // No empty slots
}

    /// <summary>
    /// Saves the current game state to a JSON file. This includes the player's position, health, 
    /// inventory, cleared encounters, and playtime.
    /// </summary>
    public void SaveGame(int saveSlot, string locationName)
    {   
        // Accumulate playtime and reset so next save won't double count time between saves
        float sessionPlayTime = Time.time - _sessionStartTime;
        _sessionStartTime = Time.time;

        // Load existing data to preserve playtime across saves
        SaveData existing = ReadSaveSlot(saveSlot);
        // If existing data exists, add the session playtime to the total playtime, otherwise start at 0
        float previousPlayTime = existing != null ? existing.totalPlayTimeSeconds : 0f;

        // Create a new SaveData object to hold essentials
        float playTime = previousPlayTime + sessionPlayTime;
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            currentHP = playerData.currentHP,
            knownAbilities = playerData.knownAbilities.ConvertAll(a => a.ToString()),
            inventoryItems = InventoryManager.Instance != null 
                ? new List<ItemData>(InventoryManager.Instance.Items)
                : new List<ItemData>(),
            clearedEncountersFlags = ProgressTracker.Instance != null 
                ? new List<string>(ProgressTracker.Instance.ClearedEncountersFlags) 
                : new List<string>(),
            storyProgressionFlags = ProgressTracker.Instance != null
                ? new List<string>(ProgressTracker.Instance.StoryProgressionFlags) 
                : new List<string>(),
            saveTimestamp = DateTime.Now.ToString("yyyy-MM-dd\nHH:mm"),
            totalPlayTimeSeconds = playTime,
            playTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}", Math.Floor(playTime/60/60), 
                                                                   Math.Floor((playTime % 3600) / 60), 
                                                                   playTime % 60),
            gameName = "",
            locationName = locationName,
            sceneName = SceneManager.GetActiveScene().name
        };

        // Write to the JSON save file
        File.WriteAllText(SlotPath(saveSlot), JsonUtility.ToJson(saveData));
        Debug.Log("Game successfully saved to slot " + saveSlot);
    }

    /// <summary>
    /// Loads the game state from a JSON file. If the file exists, 
    /// it will set the player's position and map boundary to the saved values.
    /// 
    /// returns name of scene to load to
    /// </summary>
    public string LoadGame(int slot)
    {
        SaveData saveData = ReadSaveSlot(slot);
        if(saveData == null)
        {
            Debug.Log("Failed to load save data from slot " + slot);
            return null;
        }

        // Restore everything that doesn't need the game scene objects
        playerData.currentHP = saveData.currentHP;
        playerData.knownAbilities = saveData.knownAbilities.ConvertAll(a => (Ability)Enum.Parse(typeof(Ability), a));
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.Items = new List<ItemData>(saveData.inventoryItems);
        if (ProgressTracker.Instance != null)
        {
            ProgressTracker.Instance.LoadEncounters(saveData.clearedEncountersFlags);
            ProgressTracker.Instance.LoadStoryProgression(saveData.storyProgressionFlags);
        }

        // Store position for after scene loads
        _pendingSpawnPosition = saveData.playerPosition;
        SceneManager.sceneLoaded += OnSceneLoaded;

        _sessionStartTime = Time.time;
        return saveData.sceneName;
    }

    // Helper method - used Claude code to get the idea for splitting this code off from
    // LoadGame to fix bug with objects that aren't enabled yet
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Unsubscribe immediately so this only fires once
    SceneManager.sceneLoaded -= OnSceneLoaded;

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
        player.transform.position = _pendingSpawnPosition;
    else
        Debug.LogWarning("Player missing from scene");
    }

    // Helper property that returns the file path for a given slot index
    private string SlotPath(int slot) => Application.persistentDataPath + "/savefile_" + slot + ".json";
    // Helper property that returns the file path for the current save slot
    public bool SaveSlotExists(int slot) => File.Exists(SlotPath(slot));

    /// <summary>
    /// Helper method that reads the save data from a given slot index. If the file exists, 
    /// it will return a SaveData object deserialized from the JSON file.
    /// </summary>
    /// <param name="slot">The slot index to read from</param>
    /// <returns>The saved game data, or null if no save file exists</returns>
    private SaveData ReadSaveSlot(int slot)
    {
        string path = SlotPath(slot);
        if(File.Exists(path))
        {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }
        return null;
    }

    /// <summary>
    /// Helper method that deletes the save file for a given slot index. 
    /// If the file exists, it will be removed from the persistent data path.
    /// </summary>
    /// <param name="saveSlot">The slot index to delete</param>
    public void DeleteSaveSlot(int saveSlot)
    {
        if(File.Exists(SlotPath(saveSlot)))
        {
            File.Delete(SlotPath(saveSlot));
            Debug.Log("Save slot " + saveSlot + " deleted.");
        }
        else
        {
            Debug.Log("No save file found in slot " + saveSlot + " to delete.");
        }
    }

    /// <summary>
    /// Helper method that retrieves the save data for all available slots. 
    /// (For load menu UI)
    /// </summary>
    /// <returns>Array of save data oer slot, where a null entry = empty slot</returns>
    public SaveData[] GetAllSlots()
    {
        SaveData[] allSlots = new SaveData[totalSlots]; 
        for(int i = 0; i < totalSlots; i++)
        {
            allSlots[i] = ReadSaveSlot(i);
        }
        return allSlots;
    }
}
