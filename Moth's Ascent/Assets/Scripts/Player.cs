using UnityEngine;
using UnityEngine.InputSystem;

/// <summary> 
/// Represents a player, providing basic  when exploring their top-down surroundings
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary> 
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        rb.linearVelocity = moveInput * movementSpeed;
    }

    /// <summary> 
    /// Takes in the input from the player in order to move
    /// </summary>
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
