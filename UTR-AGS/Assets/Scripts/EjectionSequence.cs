/// <summary>
/// This script controls a two-phase ejection sequence for a GameObject.
/// In Phase 1, it rises vertically and rotates until it reaches the height of a designated exit point.
/// In Phase 2, it moves horizontally toward that exit. Once it arrives, the sequence is marked as finished.
/// </summary>

using UnityEngine; // Use Unity engine functionality
public class EjectionSequence : MonoBehaviour // Define a component that manages object ejection behavior
{
    [Header("Optional reference")]
    [Tooltip("Si se deja vacío, se busca un FollowPlayer en el mismo GameObject")]
    public FollowPlayer followPlayer;   // arrástralo aquí o déjalo en blanco

    void Awake()
    {
        // Si no se asignó en el Inspector, busca en el mismo objeto
        if (followPlayer == null)
            followPlayer = GetComponent<FollowPlayer>();
    }

    // Llama a este método para iniciar la expulsión
    [ContextMenu("Start Ejection")]     // Permite probarlo desde el Inspector
    public void StartEjection()
    {
        Debug.Log($"{name}: Ejection sequence started");

        if (!isEjecting && !hasFinishedEjection)
        {
            isEjecting = true;       // Activate ejection
            ejectionTimer = 0f;      // Reset timer
        }

        if (followPlayer != null)
            followPlayer.enabled = false;  // ← solo este componente se desactiva
    }                 // Permite probarlo desde el Inspector
    [Header("Exit point (outside the window)")]
    public Transform windowExitPoint; // The destination point the object will fly toward

    [Header("Animation parameters")]
    public float ejectionForce = 5f;       // Speed of upward movement in phase 1
    public float rotationSpeed = 90f;      // Rotation speed in degrees per second
    public float moveSpeed = 4f;           // Speed of movement toward the exit point in phase 2
    public float delayBeforeEject = 0.5f;  // Duration of phase 1 before transitioning to phase 2

    private bool isEjecting = false;       // Flag to track if the ejection has started
    private float ejectionTimer = 0f;      // Timer to manage phase transitions

    public bool hasFinishedEjection { get; private set; } = false; // Public read-only flag to signal completion

    void Update()
    {
        // If not ejecting or already finished, skip processing
        if (!isEjecting || hasFinishedEjection) return;

        // Accumulate time since ejection started
        ejectionTimer += Time.deltaTime;

        if (ejectionTimer < delayBeforeEject)
        {
            // === PHASE 1: Vertical lift (limited by max height) and rotation ===

            float maxHeight = windowExitPoint.position.y; // Maximum vertical position based on exit point

            // Move upward only if current Y is below the max height
            if (transform.position.y < maxHeight)
            {
                transform.position += Vector3.up * ejectionForce * Time.deltaTime;
            }

            // Apply continuous rotation around the Z-axis
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // === PHASE 2: Move toward the exit position ===

            transform.position = Vector3.MoveTowards(
                transform.position,
                windowExitPoint.position,
                moveSpeed * Time.deltaTime
            );

            // When close enough to the exit, mark the sequence as complete
            if (Vector3.Distance(transform.position, windowExitPoint.position) < 0.1f)
            {
                isEjecting = false;
                hasFinishedEjection = true;

                Destroy(gameObject);
            }
        }
    }

    // Public method to trigger the ejection sequence from other scripts
}