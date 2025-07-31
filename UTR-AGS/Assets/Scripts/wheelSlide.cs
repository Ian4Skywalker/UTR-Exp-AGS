using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class wheelSlide : MonoBehaviour
{
    Rigidbody wheelRigidbody;
    Collider wheelCollider;
    Transform wheelTransform;
    public float slideTop, slideBottom,threshold;
    public bool isSlide, isGrabbing;

    public GameObject[] gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        wheelRigidbody = GetComponent<Rigidbody>();
        wheelCollider = GetComponent<Collider>();
        wheelTransform = GetComponent<Transform>();
        gameController = GameObject.FindGameObjectsWithTag("GameController");
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        Debug.DrawLine(gameController[0].transform.position, gameController[1].transform.position, Color.red);
    }
    // Update is called once per frame
    void Update()
    {


        if (this.transform.position.y > slideTop)
        {
            this.transform.position = new Vector3(this.transform.position.x, slideTop, this.transform.position.z);
        }
        else if (this.transform.position.y < slideBottom)
        {
            this.transform.position = new Vector3(this.transform.position.x, slideBottom, this.transform.position.z);
        }
        if (isSlide)
        {
            this.transform.position = new Vector3(this.transform.position.x, slideBottom, this.transform.position.z);
        }else if (!isGrabbing && !isSlide)
        {
            this.transform.position = new Vector3(this.transform.position.x, slideTop, this.transform.position.z);
        }
       


    }
    bool oneEntered;
    Ray ray;
    void OnTriggerStay(Collider other)
    {
        if (!isSlide)
        {


            if (other.gameObject.CompareTag("GameController"))
            {

                if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
                {

                    isGrabbing = true;
                    this.transform.position = new Vector3(this.transform.position.x, other.gameObject.transform.position.y, this.transform.position.z);
                    if (this.transform.position.y < threshold)
                    {
                        isSlide = true;
                    }

                }
                else
                {
                    isGrabbing = false;
                }


            }

        }
        else
        {
            if (other.gameObject.CompareTag("GameController"))
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                {
                    ray = new Ray(this.transform.position, other.gameObject.transform.position);
                }
                if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
                {
                    Physics.Raycast(ray);
                    Debug.DrawRay(ray.origin, ray.direction, Color.green);
                    Physics.Raycast(this.transform.position, other.gameObject.transform.position);
                    Debug.DrawRay(this.transform.position, other.gameObject.transform.position, Color.blue);
                }
                else
                {
                    isGrabbing = false;
                }

            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            isGrabbing = false;
            oneEntered = false;
         }
    }

}
