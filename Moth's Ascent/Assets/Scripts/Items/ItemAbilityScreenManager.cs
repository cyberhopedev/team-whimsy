using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
    private TMP_Text AttackName;
    [SerializeField]
    private TMP_Text AttackDescription;
    [SerializeField]
    private Image AttackImage;
    // Button for option 3
    [SerializeField]
    private Button AttackButton;

    // Assign in inspector so we can add the chosen ability/attack to the player's data
    [SerializeField] private PlayerData playerData;

    // Tracks what's being offered this popup
    private Ability _offeredAbility1;
    private Ability _offeredAbility2;
    private Attack _offeredAttack;
    private Action _onChosen;

    public void displayOptions(Ability ability1, Ability ability2, Attack attack)
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
        AttackName.text = attack.GetName();
        AttackDescription.text = attack.GetDescription();
        AttackImage.sprite = attack.GetIcon();
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
        // Pick two distinct random abilities and one random item
        Ability[] allAbilities = (Ability[])Enum.GetValues(typeof(Ability));
        List<Ability> abilityPool = new List<Ability>(allAbilities);

        int i1 = UnityEngine.Random.Range(0, abilityPool.Count);
        _offeredAbility1 = abilityPool[i1];
        abilityPool.RemoveAt(i1);

        int i2 = UnityEngine.Random.Range(0, abilityPool.Count);
        _offeredAbility2 = abilityPool[i2];
        // abilityPool.RemoveAt(i2);

        // Items[] allItems = (Items[])Enum.GetValues(typeof(Items));
        // _offeredItem = allItems[Random.Range(0, allItems.Length)];
        Attack[] allAttacks = (Attack[])Enum.GetValues(typeof(Attack));
        _offeredAttack = allAttacks[UnityEngine.Random.Range(0, allAttacks.Length)];

        // Populate the UI
        displayOptions(_offeredAbility1, _offeredAbility2, _offeredAttack);

        // Connect to the buttons, clearing any old listeners first
        Ability1Button.onClick.RemoveAllListeners();
        Ability2Button.onClick.RemoveAllListeners();
        AttackButton.onClick.RemoveAllListeners();

        Ability1Button.onClick.AddListener(() => OnChosenAbility(_offeredAbility1));
        Ability2Button.onClick.AddListener(() => OnChosenAbility(_offeredAbility2));
        AttackButton.onClick.AddListener(() => OnChosenAttack(_offeredAttack));
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
        StartCoroutine(ReturnAfterReward());
        Finish();
    }

    /// <summary>
    /// Adds the chosen attack to the player's attacks then closes the popup
    /// </summary>
    /// <param name="attack">The attack chosen</param>
    private void OnChosenAttack(Attack attack)
    {
        Debug.Log("Attack chosen: " + attack.GetName());
        // Add the chosen attack to the player's data
        playerData.LearnAttack(attack);
        Finish();
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
        StartCoroutine(ReturnAfterReward());
    }

    /// <summary>
    /// After player picks an option, hide the popup and load the
    /// overworld
    /// </summary>
    /// <returns>The coroutine to yield</returns>
    private IEnumerator ReturnAfterReward()
    {
        yield return SceneManager.UnloadSceneAsync(gameObject.scene);
        SceneManager.LoadScene(BattleSystem.overworldScene);
    }
}
