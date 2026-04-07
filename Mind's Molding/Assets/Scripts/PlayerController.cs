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

        // DontDestroyOnLoad(gameObject);
    }

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Public getter for speed of the player
    public float GetPlayerSpeed()
    {
        return movementSpeed;
    }

    // Public setter for the speed of the player
    public void SetPlayerSpeed(float speed)
    {
        movementSpeed = speed;
    }
    
    /// <summary> 
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        rb.linearVelocity = moveInput * movementSpeed;
        

        // Check for escape press (for pause menu)
        // if (Keyboard.current.escapeKey.wasPressedThisFrame)
        // {
        //     showPauseMenu = !showPauseMenu;
        //     PauseMenu.Instance.gameObject.SetActive(showPauseMenu);
        //     // Pause game while in menu
        //     Time.timeScale = showPauseMenu ? 0 : 1;
        // } 
    }

    /// <summary> 
    /// Takes in the input from the player in order to move
    /// </summary>
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // For communicating with PauseMenu when to close menu
    // public void ClosePauseMenu()
    // {
    //     showPauseMenu = false;
    //     PauseMenu.Instance.gameObject.SetActive(false);
    //     // Pause game while in menu
    //     Time.timeScale = 1;
    // }
}