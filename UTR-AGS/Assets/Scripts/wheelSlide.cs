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
    public Vector3 test;
    public GameObject[] gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    void Start()
    {
        test = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        wheelRigidbody = GetComponent<Rigidbody>();
        wheelCollider = GetComponent<Collider>();
        wheelTransform = GetComponent<Transform>();
        gameController = GameObject.FindGameObjectsWithTag("GameController");
    }
    void FixedUpdate()
    {
       
        Debug.DrawRay(new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+0.6f), gameController[0].transform.position, Color.blue);
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
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                {
                    ray = new Ray(new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z+0.6f), other.transform.position);
                    Debug.DrawRay(ray.origin, ray.direction, Color.green,5f);
                }
                if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
                {
                    //Physics.Raycast(ray);  
                    
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
