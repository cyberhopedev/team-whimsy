using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary> 
/// Represents a player, providing basic  when exploring their top-down surroundings
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] public GameObject PauseMenu;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool showPauseMenu;
    public static PlayerController Instance;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        // DontDestroyOnLoad(gameObject); <-- messing with BattleSystem initialization
    }

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set default controls to WASD and arrow keys
        SetControls(1);
    }

    /// <summary> 
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        rb.linearVelocity = moveInput * movementSpeed;

        // Check for escape press (for pause menu)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            showPauseMenu = !showPauseMenu;
            PauseMenu.SetActive(showPauseMenu);
            // Pause game while in menu
            Time.timeScale = showPauseMenu ? 0 : 1;
        } 

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log("a pressed");
            Debug.Log("Current Action Map: " + GetComponent<PlayerInput>().currentActionMap.name);
            Debug.Log(GetComponent<PlayerInput>().actions.FindActionMap("PlayerArrowKeys").enabled);
             Debug.Log(GetComponent<PlayerInput>().actions.FindActionMap("PlayerWASD").enabled);
        } 
    }

    /// <summary> 
    /// Takes in the input from the player in order to move
    /// </summary>
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("move");
    }

    // Switch between arrow keys and WASD
    public void SetControls(int state)
    {
        var actions = GetComponent<PlayerInput>().actions;

        switch (state)
        {
            case 0: // Arrow Keys (default)
                actions.FindActionMap("PlayerArrowKeys").Enable();
                actions.FindActionMap("PlayerWASD").Disable();
                break;
            case 1: // WASD
                actions.FindActionMap("PlayerArrowKeys").Disable();
                actions.FindActionMap("PlayerWASD").Enable();
                Debug.Log("set");
                break;
            case 2: // both
                actions.FindActionMap("PlayerArrowKeys").Enable();
                actions.FindActionMap("PlayerWASD").Enable();
                break;
        }
    }

    // For communicating with PauseMenu when to close menu
    public void ClosePauseMenu()
    {
        showPauseMenu = false;
        PauseMenu.SetActive(showPauseMenu);
        // Pause game while in menu
        Time.timeScale = showPauseMenu ? 0 : 1;
    }
}
