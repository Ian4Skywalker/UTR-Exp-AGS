using Oculus.Interaction;
using UnityEngine;
/// <summary>
/// Manages the interaction between a ticket and the player.
/// The ticket activates when the player enters the trigger zone and deactivates upon exit.
/// </summary>
public class TicketActivation : MonoBehaviour
{
    // Reference to the ticket object
    [SerializeField] private GameObject ticket;

    private void Start()
    {
        // Ensures the ticket is inactive at the start
        ticket.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activates the ticket when the player enters the collision area
        if (other.CompareTag("mainPlayer"))
        {
            ticket.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Deactivates the ticket when the player leaves the collision area
        if (other.CompareTag("mainPlayer"))
        {
            ticket.SetActive(false);
        }
    }
}


