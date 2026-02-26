using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target for the camera to follow (the player)
    public Transform target;
    // Offset from the target to maintain a consistent view
    public Vector3 offset;

    // LateUpdate is called after all Update functions have been called, ensuring the target has moved
    void LateUpdate()
    {
        if(target != null)
        {
            // Update the camera's position to follow the target with the specified offset
            transform.position = target.position + offset;
        }
    }
}
