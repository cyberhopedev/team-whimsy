using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    // Attack Buttons
    public Button ability1;
    public Button ability2;
    public Button ability3;
    public Button ability4;
    public Button ability5;
    public Button ability6;
    public Button ability7;
    public Button ability8;
    public Button flee;

    // Fighter stats
    public TextMeshProUGUI MothSpeed;
    public TextMeshProUGUI EnemySpeed;
    [SerializeField] private TextMeshProUGUI tooSlowMessage; 

    // Health Bars
    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    // Player Instance
    private PlayerBattler player;
    // Enemy Instance
    private Enemy enemy;

    [SerializeField] private PlayerData playerData;

    // // Player's currently assigned moves — get from PlayerData later !!!!!!!!!!!!!
    // [SerializeField] private Ability move1;
    // [SerializeField] private Ability move2;
    // [SerializeField] private Ability move3;

    private List<Ability> knownAbilities;

    private List<Button> moveButtons;
    private List<TextMeshProUGUI> buttonTexts;
    private List<Image> buttonImages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BattleSystem.OnActiveTurnChanged.AddListener(OnBattleStateChanged);

        // Assign player and enemy
        player = BattleSystem.Instance.Player;
        enemy = BattleSystem.Instance.enemies[0];  // hard coded, fix this later !!!!!

        // Set up health sliders for Player and Enemy Battler classes
        player.healthBar = playerHealthBar;
        enemy.healthBar = enemyHealthBar;

        // Get the list of available moves
        knownAbilities = playerData.getKnownAbilities();

        // Set up buttons + their text + their images
        moveButtons = new List<Button> {ability1, ability2, ability3, ability4, 
                                        ability5, ability6, ability7, ability8};
        buttonTexts = new List<TextMeshProUGUI>();
        buttonImages = new List<Image>();
        for (int i = 0; i < moveButtons.Count; i++)
        {
            buttonTexts.Add(moveButtons[i].GetComponentInChildren<TextMeshProUGUI>());
            buttonImages.Add(moveButtons[i].GetComponentInChildren<Image>());
        }

        // Text fields
        MothSpeed.text = (player.speedStat).ToString();
        EnemySpeed.text = (enemy.speedStat).ToString();
        tooSlowMessage.text = "";
        
        // Attack strategy chosen - set up buttons
        for (int i = 0; i < moveButtons.Count; i++)
        {
            int idx = i;
            moveButtons[i].onClick.AddListener(() => OnAttackChosen(idx));
        }
        flee.onClick.AddListener(() => Flee());

        // All buttons start on disabled (they get enabled as knownAbilities increases)
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].interactable = false;   
        }
    }

    void OnBattleStateChanged(BattleState state)
    {
        Debug.Log($"OnBattleStateChanged called with state: {state}");
        player = BattleSystem.Instance.Player;
        enemy = BattleSystem.Instance.FirstEnemy;

        // Hide too slow message
        tooSlowMessage.text = "";


        // Always assign health bars regardless of whose turn it is
        player.healthBar = playerHealthBar;
        enemy.healthBar = enemyHealthBar;
        playerHealthBar.maxValue = player.maxHP;
        enemyHealthBar.maxValue = enemy.maxHP;
        playerHealthBar.value = player.currentHP;
        enemyHealthBar.value = enemy.currentHP;


        if (state == BattleState.PLAYERTURN)
        {
            // Enable buttons
            for (int i = 0; i < knownAbilities.Count; i++)
            {
                moveButtons[i].interactable = true;   
            }
            flee.interactable = true;

            // Set up health bars here since enemy now exists
            player.healthBar = playerHealthBar;
            enemy.healthBar = enemyHealthBar;
            ShowMenu(player);
        }
        else
        {
            // Disable buttons during enemy turn
            for (int i = 0; i < moveButtons.Count; i++)
            {
                moveButtons[i].interactable = false;   
            }
            flee.interactable = false;
        }
    }

// Make sure to stop listening to turns once destroyed
    void OnDestroy()
    {
        BattleSystem.OnActiveTurnChanged.RemoveListener(OnBattleStateChanged);
        // your existing player unsubscribe code can be removed since we no longer hook OnStartTurn directly
    }

    // Lists all the current attack options for the player
    // lock icon will be shown on not yet unlocked abilities
    public void ListAttacks()
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            if (i < knownAbilities.Count)
            {
                buttonTexts[i].text = knownAbilities[i].GetName();
                buttonImages[i].sprite = Resources.Load<Sprite>("Sprites/Transparent Sprite 1");
            } else
            {
                buttonTexts[i].text = "";
                buttonImages[i].sprite = Ability.NONE.GetIcon();
            }
        }
    }

    /// <summary>
    /// Call when fleeing the battle, have a chance to flee and when successful,
    /// return to the overworld scene, if not, enemy gets an extra turn and stay
    /// in the battle scene
    /// </summary>
    public void Flee()
    {
        // if(Random.value < 0.4f) // 40% chance to flee successfully
        // {
        //     Debug.Log("Flee successful!");
        //     BattleSystem.Instance.Flee();
        // }
        // else
        // {
        //     tooSlowMessage.text = "Too Slow!";
        //     Debug.Log("Flee failed! Enemy gets an extra turn.");
        //     BattleSystem.Instance.FleeFailure();
        // }
        BattleSystem.Instance.ChoseAttackMove(Ability.TESTING_CHEAT, enemy);
    }

    // Show menu and list attacks/stats
    void ShowMenu(PlayerBattler p)
    {
        gameObject.SetActive(true);
        ListAttacks(); 
    }

    // Hides the menu
    void HideMenu()
    {
        gameObject.SetActive(false);
    }

    // Performs action based on choice
    void OnAttackChosen(int attackChoice)
    {
        // Alert something of attack chosen
        Debug.Log("attack chosen!");

        // Do something based on attack
        Debug.Log("attack " + attackChoice + " chosen!");
        BattleSystem.Instance.ChoseAttackMove(knownAbilities[attackChoice], enemy);
        // switch (attackChoice)
        // {
        //     case 0:
        //     // Struggle, Claw, Raise Arms
        //         Debug.Log("attack 1 chosen!");
        //         BattleSystem.Instance.ChoseAttackMove(move1, enemy);
        //         break;
        //     case 1:
        //         Debug.Log("attack 2 chosen!");
        //         BattleSystem.Instance.ChoseAttackMove(move2, enemy);
        //         break;
        //     case 2:
        //         Debug.Log("attack 3 chosen!");
        //         BattleSystem.Instance.ChoseAbility(move3);
        //         break;
        //     case 3: // flee
        //     Debug.Log("flee chosen!");
        //         Flee();
        //         break;
        // }
    }
} 
