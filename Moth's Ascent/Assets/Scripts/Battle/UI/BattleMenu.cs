using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    // Attack Buttons
    public TextMeshProUGUI attack1Text;
    public TextMeshProUGUI attack2Text;
    public TextMeshProUGUI attack3Text;
    public Button attack1;
    public Button attack2;
    public Button attack3;
    public Button flee;

    // Fighter stats
    public TextMeshProUGUI MothSpeed;
    public TextMeshProUGUI EnemySpeed;

    // Health Bars
    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    // Player Instance
    private PlayerBattler player;
    // Enemy Instance
    private Enemy enemy;

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

        // Text fields
    //     attack1Text.text = "";
    //     attack2Text.text = "";
    //     attack3Text.text = "";
    //     MothSpeed.text = (player.speedStat).ToString();
    //     EnemySpeed.text = (enemy.speedStat).ToString();
        
        // Attack strategy chosen
        attack1.onClick.AddListener(() => OnAttackChosen(0));
        attack2.onClick.AddListener(() => OnAttackChosen(1));
        attack3.onClick.AddListener(() => OnAttackChosen(2));
        flee.onClick.AddListener(() => OnAttackChosen(3));
        
        // HideMenu();
    }

    void OnBattleStateChanged(BattleState state)
    {
        Debug.Log($"OnBattleStateChanged called with state: {state}");
        player = BattleSystem.Instance.Player;
        enemy = BattleSystem.Instance.FirstEnemy;


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
            attack1.interactable = true;
            attack2.interactable = true;
            attack3.interactable = true;
            flee.interactable = true;

            // Set up health bars here since enemy now exists
            player.healthBar = playerHealthBar;
            enemy.healthBar = enemyHealthBar;
            ShowMenu(player);
        }
        else
        {
            // Disable buttons during enemy turn
            attack1.interactable = false;
            attack2.interactable = false;
            attack3.interactable = false;
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
    public void ListAttacks(/*List<AttackType> attacks*/)
    {
        attack1Text.text = "Struggle";
        attack2Text.text = "Claw";
        attack3Text.text = "Raise Arms";
    }

    // Perform the attack
    public void AttackAction()
    {
        // do something 
    }

    // public void Flee()
    // {
        
    // }

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
        switch (attackChoice)
        {
            case 0:
            // Struggle, Claw, Raise Arms
                Debug.Log("attack 1 chosen!");
                break;
            case 1:
                Debug.Log("attack 2 chosen!");
                // enemy.TakeDamage(5);
                break;
            case 2:
                Debug.Log("attack 3 chosen!");
                break;
            case 3: // flee
            Debug.Log("flee chosen!");
                BattleSystem.Instance.Flee();
                break;
        }

        BattleSystem.Instance.ChoseAttack(enemy);
    }
} 
