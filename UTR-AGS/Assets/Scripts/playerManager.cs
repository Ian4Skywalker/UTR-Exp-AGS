using UnityEngine;
public class playerManager : MonoBehaviour
{
    
    public Rigidbody playerBody;
    public Vector2 Axis;
    public float playerSpeed=2;
    public Transform currentCameraPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Axis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        Vector3 forward = currentCameraPosition.forward;
        Vector3 right = currentCameraPosition.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * Axis.y) + (right * Axis.x);


        playerBody.linearVelocity = direction * playerSpeed;
        
    }
    public void Moving()
    {
        
    }
}
