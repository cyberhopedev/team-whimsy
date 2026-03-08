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
    [SerializeField]
    private TMP_Text Ability1Name;
    [SerializeField]
    private TMP_Text Ability1Description;
    [SerializeField]
    private Image Ability1Image;
    [SerializeField]
    private TMP_Text Ability2Name;
    [SerializeField]
    private TMP_Text Ability2Description;
    [SerializeField]
    private Image Ability2Image;
    [SerializeField]
    private TMP_Text AttackName;
    [SerializeField]
    private TMP_Text AttackDescription;
    [SerializeField]
    private Image AttackImage;

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
        
    }

    /// <summary>
    /// Adds the chosen ability to the player's abilities then closes the popup
    /// </summary>
    /// <param name="ability">The ability chosen</param>
    private void OnChosenAbility(Ability ability)
    {
        Debug.Log("Ability chosen: " + ability.GetName());
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
        Finish();
    }

    /// <summary>
    /// Adds the chosen item to the player's inventory then closes the popup
    /// </summary>
    /// <param name="item">The item chosen</param>
    private void OnChosenItem(Item item)
    {
        Debug.Log("Item chosen: " + item.GetName());
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
