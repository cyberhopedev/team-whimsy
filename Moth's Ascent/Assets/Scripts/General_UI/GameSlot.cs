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
    private string gameName;
    private string location;
    private string timePlayed;
    private string lastSaved;

    // UI Text
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Location;
    [SerializeField]
    private TMP_Text TimePlayed;
    [SerializeField]
    private TMP_Text LastSaved;
    [SerializeField]
    private GameObject selectedHighlight;
    // Disables slot buttons when they are empty
    private Boolean buttonDisabled;

    // Constructor
    public void Start()
    {
        this.gameName = null;
        this.location = null;
        this.timePlayed = null;
        this.lastSaved = null;
        selectedHighlight.SetActive(false);
    }

    public Boolean IsButtonDisabled()
    {
        return buttonDisabled;
    }

    // For setting highlighted background when clicked
    public void ClickSlot()
    {
        selectedHighlight.SetActive(true);
    }

    public void UnclickSlot()
    {
        selectedHighlight.SetActive(false);
    }

    public void FillInSlot(SaveData game)
    {
        // Get info
        this.gameName = game.gameName;
        this.location = game.locationName;
        this.timePlayed = game.playTimeString;
        this.lastSaved = game.saveTimestamp;

        // Update UI
        Name.text = gameName;
        Location.text = location;
        TimePlayed.text = timePlayed;
        LastSaved.text = lastSaved;

        buttonDisabled = false;
    }

    public void ShowEmpty()
    {
        Name.text = "Empty";
        Location.text = "";
        TimePlayed.text = "";
        LastSaved.text = "";
        buttonDisabled = true;
    }
    
}
