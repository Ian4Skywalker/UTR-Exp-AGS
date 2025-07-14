/*
    PlayerManager Script

    This script controls player movement in a VR environment using the thumbstick input from an Oculus controller.
    It dynamically calculates movement direction based on the camera's orientation, ensuring intuitive navigation.
    The player's Rigidbody velocity is updated accordingly to move the player smoothly.
*/

using UnityEngine; 
using UnityEngine.UI; 
public class playerManager : MonoBehaviour
{
    public Rigidbody playerBody; // Reference to the player's Rigidbody for physics-based movement.
    public Vector2 Axis; // Stores input values from the Oculus controller's thumbstick.
    public float playerSpeed = 2; // Movement speed multiplier.
    public Transform currentCameraPosition; // Reference to the VR camera's position.
    public Image panel; // UI panel (possibly for scene transitions or status indicators).

    void Update()
    {
        // Retrieves the thumbstick movement input from the Oculus controller.
        Axis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        // Determines the forward and right directions based on the camera's orientation.
        Vector3 forward = currentCameraPosition.forward;
        Vector3 right = currentCameraPosition.right;

        // Ensures movement remains horizontal by nullifying the y-axis values.
        forward.y = 0;
        right.y = 0;

        // Normalizes vectors to maintain consistent directional movement.
        forward.Normalize();
        right.Normalize();

        // Computes the final movement direction based on thumbstick input 👍👍👍👍.
        Vector3 direction = (forward * Axis.y) + (right * Axis.x);

        // Applies movement to the player's Rigidbody.
        playerBody.linearVelocity = direction * playerSpeed;
    }
}