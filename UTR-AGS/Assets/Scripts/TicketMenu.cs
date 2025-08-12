using UnityEngine;
using TMPro;

/// <summary>
/// Manages a simple ticket interaction menu where the player selects "Yes" or "No".
/// If the correct button is pressed, the ticket is processed, and the associated objects are removed.
/// If the wrong button is chosen, the incorrect button is visually enlarged to indicate error.
/// Optionally triggers an ejection sequence on correct "No" responses.
/// </summary>
public class TicketMenu : MonoBehaviour
{
    // UI references to the "Yes" and "No" buttons
    public GameObject yesButton;
    public GameObject noButton;

    // Reference to the DoorManager2 script that keeps track of correct answers
    public DoorManager2 doorManager;

    // GameObjects that are destroyed once the ticket has been answered correctly
    public GameObject ticket;     // The ticket object itself
    public GameObject ticketUI;   // The UI canvas or panel
    public GameObject ticketCol;  // The ticket's collision detection object

    // Whether "Yes" is the correct answer for this ticket
    public bool isCorrectButton = true;

    // Scale used to visually highlight an incorrect button
    private Vector3 enlargedScale = Vector3.one * 1.5f;

    // Cached references to UI transforms for visual scaling
    private RectTransform yesRect;
    private RectTransform noRect;

    // Optional reference to an ejection sequence script (e.g., visual or gameplay feedback)
    public EjectionSequence ejectionScript;

    /// <summary>
    /// Initialization: retrieves RectTransform components for button scaling.
    /// </summary>
    private void Start()
    {
        yesRect = yesButton.GetComponent<RectTransform>();
        noRect = noButton.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Handles logic when the "Yes" button is clicked.
    /// If correct, destroys ticket components and updates the correct count.
    /// If incorrect, visually emphasizes the "No" button.
    /// </summary>
    public void OnYesClicked()
    {
        if (isCorrectButton)
        {
            doorManager.RegisterCorrectTicket();  // Increment door logic

            Destroy(ticketCol);
            Destroy(ticket);
            Destroy(ticketUI);
        }
        else
        {
            noRect.localScale = enlargedScale;
        }
    }

    /// <summary>
    /// Handles logic when the "No" button is clicked.
    /// If correct, destroys ticket components, updates the door, and triggers optional ejection sequence.
    /// If incorrect, visually emphasizes the "Yes" button.
    /// </summary>
    public void OnNoClicked()
    {
        if (!isCorrectButton)
        {
            doorManager.RegisterCorrectTicket();  // Increment door logic

            Destroy(ticketCol);
            Destroy(ticket);
            Destroy(ticketUI);

            ejectionScript.StartEjection();  // Optional visual or audio effect
        }
        else
        {
            yesRect.localScale = enlargedScale;
        }
    }
}