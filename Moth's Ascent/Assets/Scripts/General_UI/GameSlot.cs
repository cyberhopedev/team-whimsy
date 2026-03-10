using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Slot for each game in the load menu - essentially the UI display of
// SaveData class
public class GameSlot : MonoBehaviour
{
    // Game Data
    public string gameName;
    public string location;
    public string timePlayed;
    public string lastSaved;

    // UI Text
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Location;
    [SerializeField]
    private TMP_Text TimePlayed;
    [SerializeField]
    private TMP_Text LastSaved;

    public void FillInSlot(SaveData game)
    {
        // Get info
        this.gameName = game.locationName;
        this.location = game.locationName;
        this.timePlayed = game.playTimeString;
        this.lastSaved = game.saveTimestamp;

        // Update UI
        Name.text = gameName;
        Location.text = location;
        TimePlayed.text = timePlayed;
        LastSaved.text = lastSaved;
    }
    
}
