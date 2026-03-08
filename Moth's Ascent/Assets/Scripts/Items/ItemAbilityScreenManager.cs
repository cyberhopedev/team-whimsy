using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemAbilityScreenManager : MonoBehaviour
{
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
}
