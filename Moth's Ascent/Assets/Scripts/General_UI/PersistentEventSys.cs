using UnityEngine;
using UnityEngine.EventSystems;

// Makes it so that there's one common EventSystem object the entire
// game and will get rid of any duplicates
public class PersistentEventSystem : MonoBehaviour
{
    private void Start()
    {
        // Destroy any duplicate EventSystems in the loaded scene
        EventSystem[] systems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        if (systems.Length > 1)
        {
            foreach (EventSystem system in systems)
            {
                if (system.gameObject != this.gameObject)
                    Destroy(system.gameObject);
            }
        }

        // DontDestroyOnLoad(gameObject);
    }
}