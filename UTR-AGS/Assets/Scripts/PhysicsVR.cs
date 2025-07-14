/*
    PhysicsVR Script

    This script dynamically adjusts the height and center position of a player's CapsuleCollider 
    in a VR environment based on the vertical position of the VR camera.
    It ensures that the player's collider reflects their real-world movement, 
    preventing unnatural clipping and improving immersion.
*/

using UnityEngine; 

public class PhysicsVR : MonoBehaviour
{
    public Transform currentCameraPosition; // Reference to the VR camera's position.
    public CapsuleCollider playerCollision; // Capsule collider that represents the player's physical bounds.
    public float maxHeight; // Maximum height the player's collider can reach.
    public float minHeight; // Minimum height the player's collider can shrink to.

    void Update()
    {
        // Adjusts the height of the player's collider based on their head position (VR camera).
        // Ensures the height stays within a valid range defined by minHeight (enano) and maxHeight (poste).
        playerCollision.height = Mathf.Clamp(currentCameraPosition.localPosition.y, minHeight, maxHeight);

        // Updates the center of the collider to match the player's position.
        // This ensures that the collider properly surrounds the player, maintaining realistic movement physics.
        playerCollision.center = new Vector3(currentCameraPosition.localPosition.x, playerCollision.height / 2, currentCameraPosition.localPosition.z);
    }
}