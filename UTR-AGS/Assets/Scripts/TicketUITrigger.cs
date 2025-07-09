using UnityEngine;
/// <summary>
/// This script manages the UI of a ticket in Unity.
/// When the player enters the activation area, the ticket UI is displayed.
/// When the player exits the area, the UI is hidden.
/// </summary>

public class TicketUITrigger : MonoBehaviour
{
    // Reference to the GameObject representing the ticket UI, assigned from the Inspector.
    [SerializeField] private GameObject ticketUI;

    /// <summary>
    /// Method called when another Collider enters the trigger.
    /// If the object is the player, it activates the ticket UI.
    /// </summary>
    /// <param name="other">The Collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object entering the trigger has the "mainPlayer" tag.
        if (other.CompareTag("mainPlayer"))
        {
            // Activates the ticket UI.
            ticketUI.SetActive(true);
        }
    }

    /// <summary>
    /// Method called when another Collider exits the trigger.
    /// If the object is the player, it deactivates the ticket UI.
    /// </summary>
    /// <param name="other">The Collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        // Checks if the object exiting the trigger has the "mainPlayer" tag.
        if (other.CompareTag("mainPlayer"))
        {
            // Deactivates the ticket UI.
            ticketUI.SetActive(false);
        }
    }
}

