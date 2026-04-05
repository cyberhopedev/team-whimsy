using UnityEngine;
using Unity.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class MagicBook : MonoBehaviour
{
    // Book pages
    [SerializeField] GameObject rightPage;
    [SerializeField] GameObject leftPage;
    // Descriptions
    [SerializeField] TextMeshProUGUI leftDescription;
    [SerializeField] TextMeshProUGUI rightDescription;
    // Runes
    [SerializeField] Image image1L;
    [SerializeField] Image image2L;
    [SerializeField] Image image3L;
    [SerializeField] Image image1R;
    [SerializeField] Image image2R;
    [SerializeField] Image image3R;
    // Translations
    [SerializeField] TextMeshProUGUI t1L;
    [SerializeField] TextMeshProUGUI t2L;
    [SerializeField] TextMeshProUGUI t3L;
    [SerializeField] TextMeshProUGUI t1R;
    [SerializeField] TextMeshProUGUI t2R;
    [SerializeField] TextMeshProUGUI t3R;
    // Header on right page (disappears when page is blank)
    [SerializeField] TextMeshProUGUI descripHeader;
    // For grouping these fields together in code
    private Image[] leftImages;
    private Image[] rightImages;
    private TextMeshProUGUI[] leftTranslations;
    private TextMeshProUGUI[] rightTranslations;


    // What spell is currently showing on the left page (right is just the next spell)
    private int leftIdx = 0;
    private int LEFT_IDX_MAX = 4;
    // Page content
    private Spells[] spells = {Spells.SPELL_1, Spells.SPELL_2, Spells.SPELL_3, Spells.SPELL_4, Spells.SPELL_5, Spells.BLANK};
    private List<Sprite> runes = new List<Sprite>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize all rune sprites
        for (int i = 1; i < 16; i++)
        {
            runes.Add(Resources.Load<Sprite>("Sprites/rune" + i));
        }
        runes.Add(Resources.Load<Sprite>("Sprites/TransparentSprite"));

        // Group together page runes and descriptions
        leftImages = new Image[] {image1L, image2L, image3L};
        rightImages = new Image[] {image1R, image2R, image3R};
        leftTranslations = new TextMeshProUGUI[] {t1L, t2L, t3L};
        rightTranslations = new TextMeshProUGUI[] {t1R, t2R, t3R};

        // Open book to page 1
        leftIdx = -2; // will be set to 0 after this
        FlipPage(1);
    }

    // Flips the page left or right (-1 is left, 1 is right)
    public void FlipPage(int direction)
    {
        // Flip to the next page if possible
        if (direction > 0 && leftIdx < LEFT_IDX_MAX)
        {
            leftIdx += 2;
        } else if (direction < 0 && leftIdx > 0)  // flip backwards if possible
        {
            leftIdx -= 2;
        }

        // Update rune symbols
        int i = 0;
        foreach (int idx in spells[leftIdx].GetSprite())
        {
            leftImages[i].sprite = runes[idx];
                i++;
        }
        i = 0;
        foreach (int idx in spells[leftIdx + 1].GetSprite())
        {
            rightImages[i].sprite = runes[idx];
                i++;
        }

        // Update translations
        i = 0;
        foreach (string t in spells[leftIdx].GetTranslation())
        {
            leftTranslations[i].text = t;
            i++;
        }
        i = 0;
        foreach (string t in spells[leftIdx + 1].GetTranslation())
        {
            rightTranslations[i].text = t;
            i++;
        }

        // Update description
        leftDescription.text = spells[leftIdx].GetDescription();
        rightDescription.text = spells[leftIdx + 1].GetDescription();

        // Make header disappear for blank page
        descripHeader.text = rightDescription.text == "" ? "" : "Description:";
    }

    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }
}
