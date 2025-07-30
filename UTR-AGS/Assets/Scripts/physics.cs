using UnityEngine;

public class physics : MonoBehaviour
{
    public Transform currentCameraPosition;
    public CapsuleCollider playerCollision;
    public float maxHeight;
    public float minHeight;

    // Update is called once per frame
    void Update()
    {
        playerCollision.height = Mathf.Clamp(currentCameraPosition.localPosition.y, minHeight,maxHeight);
        playerCollision.center= new Vector3(currentCameraPosition.localPosition.x, playerCollision.height/2, currentCameraPosition.localPosition.z);
    }
}
