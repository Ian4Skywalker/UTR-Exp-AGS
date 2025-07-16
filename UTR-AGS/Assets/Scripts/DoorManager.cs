/*
    DoorManager Script

    This script is responsible for handling scene transitions when the player interacts with a door.
    It uses Unity's collision detection system to trigger a scene change when the player enters a specific area.
    A fading effect is applied to a UI panel before the scene transition occurs.
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Allows scene management for transitioning between levels.

public class DoorManager : MonoBehaviour
{
    [SerializeField] SceneField SceneNumber; // The target scene to load when the player enters the door area.

    private void Update()
    {
        // Continuously checks if scene transition should occur and starts coroutine accordingly.
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider that entered is tagged as the main player.
        if (other.CompareTag("mainPlayer"))
        {
            SceneManager.LoadScene(SceneNumber);
           
        }
    }

    
}