using UnityEngine;

/// <summary>
/// Handles all of the resources the player can use, which includes
/// magic, chalk, and berries
/// </summary>
public class ResourceManager : MonoBehaviour
{   
    // Instance of the resource manager
    public static ResourceManager Instance;

    // Resources available to the user
    [Header("Resources")]
    public int magic = 0;
    public int maxMagic = 50;

    public int chalk = 0;
    public int berries = 0;

    /// <summary>
    /// Creates the public instance of the manager
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Adds magic to the player's storage
    /// </summary>
    /// <param name="amount">The amount of magic to add</param>
    public void AddMagic(int amount)
    {
        
    }

    /// <summary>
    /// Adds chalk to the player's storage
    /// </summary>
    /// <param name="amount">The amount of chalk to add</param>
    public void AddChalk(int amount)
    {
        
    }

    /// <summary>
    /// Adds berries to the player's storage
    /// </summary>
    /// <param name="amount">The amount of berries to add</param>
    public void AddBerries(int amount)
    {

    }

    /// <summary>
    /// Checks if the player has enough magic when attempting to spend it
    /// </summary>
    /// <param name="amount">The amount of magic being spent</param>
    /// <returns>True if the player has enough, false if otherwise</returns>
    public bool SpendMagic(int amount)
    {
        return false;
    }

    /// <summary>
    /// Checks if the player has enough chalk when attempting to spend it
    /// </summary>
    /// <param name="amount">The amount of chalk being spent</param>
    /// <returns>True if the player has enough, false if otherwise</returns>
    public bool SpendChalk(int amount)
    {
        return false;
    }

    
    /// <summary>
    /// Checks if the player has enough berries when attempting to spend it
    /// </summary>
    /// <param name="amount">The amount of berries being spent</param>
    /// <returns>True if the player has enough, false if otherwise</returns>
    public bool SpendBerries(int amount)
    {
        return false;
    }
}