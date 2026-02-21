using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    public TextMeshProUGUI attack1Text;
    public TextMeshProUGUI attack2Text;
    public TextMeshProUGUI attack3Text;
    public Button attack1;
    public Button attack2;
    public Button attack3;
    public Button flee;

    public TextMeshProUGUI MothSpeed;
    public TextMeshProUGUI EnemySpeed;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack1Text.text = "thisl";
        attack2Text.text = "..";
        attack3Text.text = "temp";
        MothSpeed.text = "80";
        EnemySpeed.text = "45";

        attack1.onClick.AddListener(AttackAction);
        attack2.onClick.AddListener(AttackAction);
        attack3.onClick.AddListener(AttackAction);
        flee.onClick.AddListener(Flee);

        MothSpeed.text = "88";
        EnemySpeed.text = "99";

        // ListAttacks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Lists all the current attack options for the player
    public void ListAttacks()
    {
        // attack1Text.text = "temp";
    }

    public void AttackAction()
    {
        // do something 
    }

    public void Flee()
    {
        
    }
} 
