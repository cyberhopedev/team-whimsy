using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

// For the loot selection scene
public class ItemAbilityScreenManager : MonoBehaviour
{   
    // Makes instance of the system accesible to child classes
    public static ItemAbilityScreenManager Instance { get; private set; }

    // Option 1
    [SerializeField]
    private TMP_Text Ability1Name;
    [SerializeField]
    private TMP_Text Ability1Description;
    [SerializeField]
    private Image Ability1Image;
    // Button for option 1
    [SerializeField]
    private Button Ability1Button;

    // Option 2
    [SerializeField]
    private TMP_Text Ability2Name;
    [SerializeField]
    private TMP_Text Ability2Description;
    [SerializeField]
    private Image Ability2Image;
    // Button for option 2
    [SerializeField]
    private Button Ability2Button;

    // Option 3
    [SerializeField]
    private TMP_Text ItemName;
    [SerializeField]
    private TMP_Text ItemDescription;
    [SerializeField]
    private Image ItemImage;
    // Button for option 3
    [SerializeField]
    private Button ItemButton;

    // Assign in inspector so we can add the chosen ability/Ability to the player's data
    [SerializeField] private PlayerData playerData;

    private Action _onChosen;

    // Offered abilities and items - has two buffer none's
    private List<Ability> AbilityOrder = new List<Ability> {Ability.RAISE_ARMS, 
                                                            Ability.CLAW, Ability.ACID_SPIT, 
                                                            Ability.EXOSKELETON, Ability.FILTER_FLUFF, 
                                                            Ability.BITE, Ability.GLITTER,
                                                            Ability.NONE, Ability.NONE};

    private Queue<Item> ItemOrder = new Queue<Item>( new List<Item> {null, null} ); // TWO NONE'S !!!!!!!!!

    public void displayOptions(Ability ability1, Ability ability2, Item item1)
    {
        // Option 1
        Ability1Name.text = ability1.GetName();
        Ability1Description.text = ability1.GetDescription();
        Ability1Image.sprite = ability1.GetIcon();

        // Option 2
        Ability2Name.text = ability2.GetName();
        Ability2Description.text = ability2.GetDescription();
        Ability2Image.sprite = ability2.GetIcon();

        // Option 3
        // AbilityName.text = Ability.GetName();
        // AbilityDescription.text = Ability.GetDescription();
        // AbilityImage.sprite = Ability.GetIcon();
    }

    /// <summary>
    /// On start, show the popup with the options. onChosen is called after the player picks, 
    /// so BattleSystem can then load the overworld scene
    /// </summary>
    private void Start()
    {
        Show();
    }

    /// <summary>
    /// Called by BattleSystem.OnRewardChosen; picks random rewards,
    /// populates the UI, and wires the buttons. onChosen is called after the player picks, so BattleSystem
    /// can then load the overworld scene.
    /// </summary>
    public void Show()
    {
        // Filter out abilities the player already knows
        List<Ability> availableAbilities = AbilityOrder
            .FindAll(a => a != Ability.NONE && !playerData.knownAbilities.Contains(a));

        // Add NONE buffers if not enough abilities left
        while (availableAbilities.Count < 2)
            availableAbilities.Add(Ability.NONE);
        
        // Dispaly the next available options
        Ability _offeredAbility1 = availableAbilities[0];
        Ability _offeredAbility2 = availableAbilities[1];
        Item _offeredItem = ItemOrder.Peek();

        // Populate the UI
        displayOptions(_offeredAbility1, _offeredAbility2, _offeredItem);

        // Connect to the buttons, clearing any old listeners first
        Ability1Button.onClick.RemoveAllListeners();
        Ability2Button.onClick.RemoveAllListeners();
        ItemButton.onClick.RemoveAllListeners();

        // Set up button or make it non-interactable
        if (_offeredAbility1 == Ability.NONE)
        {
            Ability1Button.interactable = false;
        } else
        {
            Ability1Button.onClick.AddListener(() => OnChosenAbility(_offeredAbility1));
        }

        if (_offeredAbility2 == Ability.NONE)
        {
            Ability2Button.interactable = false;
        } else
        {
            Ability2Button.onClick.AddListener(() => OnChosenAbility(_offeredAbility2));
        }

        if (_offeredItem == null)
        {
            ItemButton.interactable = false;
        } else
        {
            ItemButton.onClick.AddListener(() => OnChosenItem(_offeredItem));
        }
    }

    /// <summary>
    /// Adds the chosen ability to the player's abilities then closes the popup
    /// </summary>
    /// <param name="ability">The ability chosen</param>
    private void OnChosenAbility(Ability ability)
    {
        Debug.Log("Ability chosen: " + ability.GetName());
        // Add the chosen ability to the player's data
        playerData.LearnAbility(ability);
        Finish();  //start coroutine in here
    }

    /// <summary>
    /// Adds the chosen item to the player's inventory then closes the popup
    /// </summary>
    /// <param name="item">The item chosen</param>
    private void OnChosenItem(Item item)
    {
        Debug.Log("Item chosen: " + item.GetName());
        // Add the chosen item to the player's inventory
        InventoryManager.Instance.AddItem(item.GetName(), 1, item.GetSprite());
        Finish();
    }


    /// <summary>
    /// After player picks an option, hide the popup and load the
    /// worlds
    /// </summary>
    private void Finish()
    {
        // Auto-save so the new ability persists
        if (SaveController.Instance != null)
        {
            SaveController.Instance.SaveProgressionOnly();   
        }

        // Tell BattleSystem to load overworld after we unload
        BattleSystem.Instance.ReturnToOverworldAfterReward();
        // Then unload this scene
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    /// <summary>
    /// After player picks an option, hide the popup and load the
    /// overworld
    /// </summary>
    /// <returns>The coroutine to yield</returns>
    private IEnumerator ReturnAfterReward()
    {
        yield return SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
