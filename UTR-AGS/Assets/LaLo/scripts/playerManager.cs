using UnityEngine;
public class playerManager : MonoBehaviour
{

    public Rigidbody playerBody;
    private Vector2 Axis;
    public float playerSpeed = 2;
    public Transform currentCameraPosition;
    public float rotationSpeed = 90f; // grados por segundo
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento con LThumbstick
        Axis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        Vector3 forward = currentCameraPosition.forward;
        Vector3 right = currentCameraPosition.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * Axis.y) + (right * Axis.x);

        playerBody.linearVelocity = direction * playerSpeed;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Rotación con RThumbstick
        Vector2 rThumbstick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (Mathf.Abs(rThumbstick.x) > 0.1f)
        {
            // Rotar solo en el eje Y (horizontal)
            currentCameraPosition.Rotate(0, rThumbstick.x * rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}