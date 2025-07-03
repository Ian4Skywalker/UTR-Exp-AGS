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
    public Image panel; // UI panel used for fade transition effect.
    float speed = 0f; // Speed reference used in the fade transition effect.
    bool changeSceneBool = false; // Boolean flag to indicate whether scene transition should start.

    private void Update()
    {
        // Continuously checks if scene transition should occur and starts coroutine accordingly.
        if (changeSceneBool == true)
        {
            StartCoroutine(changeScene()); // Initiates the coroutine responsible for transitioning the scene.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider that entered is tagged as the main player.
        if (other.CompareTag("mainPlayer"))
        {
            changeSceneBool = true; // Sets flag to true, initiating scene change in the Update method.
        }
    }

    IEnumerator changeScene()
    {
        // Applies a fade transition effect by modifying the panel's alpha (auuuu 🐺🐺) value.
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.SmoothDamp(0f, 255f, ref speed, .5f));

        yield return new WaitForSeconds(1.5f); // Waits for 1.5 seconds before changing scenes.

        SceneManager.LoadScene(SceneNumber); // Loads the new scene defined in SceneNumber.
    }
}