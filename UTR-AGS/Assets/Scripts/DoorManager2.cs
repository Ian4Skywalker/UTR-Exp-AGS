using UnityEngine;

/// <summary>
/// DoorManager2 controls the activation of a door in the scene based on two conditions:
/// 1. The player has answered a set number of questions (tickets) correctly.
/// 2. A maximum wait time has passed since the start of the scene.
/// The door activates once either condition is met, and only once.
/// </summary>
public class DoorManager2 : MonoBehaviour
{
    [Tooltip("Door that will be revealed")]
    public GameObject door;

    [Tooltip("Number of correct tickets required to open the door")]
    public int ticketsNeeded = 4;

    // Keeps track of how many correct answers have been registered
    private int correctCount = 0;

    // Time limit before the door opens regardless of correct answers (in seconds)
    [Tooltip("Maximum wait time before the door appears")]
    [SerializeField] private float delayTime = 30f;

    private float startTime;
    private bool doorActivated = false;

    private void Start()
    {
        // Record the time when the scene starts
        startTime = Time.time;
    }

    private void Update()
    {
        // Prevent multiple activations of the door
        if (doorActivated) return;

        // Check if the allotted time has passed
        if (Time.time - startTime >= delayTime)
        {
            ActivateDoor();
        }
    }

    /// <summary>
    /// Call this method each time a ticket is correctly answered.
    /// </summary>
    public void RegisterCorrectTicket()
    {
        // Do nothing if the door has already been activated
        if (doorActivated) return;

        correctCount++;

        // If we've reached the required number of correct tickets, open the door
        if (correctCount >= ticketsNeeded)
        {
            ActivateDoor();
        }
    }

    /// <summary>
    /// Activates the door and prevents future activations.
    /// </summary>
    private void ActivateDoor()
    {
        door.SetActive(true);
        doorActivated = true;
    }
}