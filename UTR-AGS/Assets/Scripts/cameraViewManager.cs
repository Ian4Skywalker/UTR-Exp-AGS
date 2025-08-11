using UnityEngine;

public class cameraViewManager : MonoBehaviour
{
    public GameObject objectInView;//Objected That entered in the view of the camera (colission)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("photoModel"))//The Object must have this tag
        {
            objectInView = other.gameObject;

            Debug.Log("blablabla");
        }
    }
    private void OnTriggerExit(Collider other)//Check if the object already left
    {
        if (other.gameObject.CompareTag("photoModel"))
        {
            objectInView = null;
            Debug.Log("ObjectExit");
        }
    }
}
