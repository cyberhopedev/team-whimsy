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
        // Get player
        player = BattleSystem.Instance.Player;
        enemy = BattleSystem.Instance.FirstEnemy;

        // Set up health sliders for Player and Enemy Battler classes
        player.healthBar = playerHealthBar;
        enemy.healthBar = enemyHealthBar;

        // Pay attention to start and end of turn in BattleSystem
        player.OnStartTurn += ShowMenu;
        player.OnEndTurn += HideMenu;

        // Text fields
        attack1Text.text = "";
        attack2Text.text = "";
        attack3Text.text = "";
        MothSpeed.text = (player.speedStat).ToString();
        EnemySpeed.text = (enemy.speedStat).ToString();

        // Attack strategy chosen
        attack1.onClick.AddListener(() => OnAttackChosen(0));
        attack2.onClick.AddListener(() => OnAttackChosen(1));
        attack3.onClick.AddListener(() => OnAttackChosen(2));
        flee.onClick.AddListener(() => OnAttackChosen(3));

        HideMenu();
    }

    // Lists all the current attack options for the player
    public void ListAttacks(PlayerBattler p)
    {
        attack1Text.text = "";
        attack2Text.text = "";
        attack3Text.text = "";
    }

    public void AttackAction()
    {
        // do something 
    }

    public void Flee()
    {
        
    }

    // Make sure to stop listening to turns once destroyed
    void OnDestroy()
    {
        if (player != null)
        {
            player.OnStartTurn -= ShowMenu;
            player.OnEndTurn -= HideMenu;
        }
    }

    // Show menu and list attacks/stats
    void ShowMenu(PlayerBattler p)
    {
        gameObject.SetActive(true);
        ListAttacks(p); 
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
    }
} 
