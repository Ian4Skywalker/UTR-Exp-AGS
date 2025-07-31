using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class wheelSlide : MonoBehaviour
{
    Rigidbody wheelRigidbody;
    Collider wheelCollider;
    Transform wheelTransform;
    public float slideTop, slideBottom, threshold;
    public bool isSlide, isGrabbing;
    public GameObject test;
    public GameObject[] gameController;
    Vector3 rayo1, rayo2;
    [SerializeField] float wheelCurrentPosition = 0;
    public float angle;
    [SerializeField] int checkPoints = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }
    void Start()
    {
        test = GameObject.Find("wheelPosition");
        wheelRigidbody = GetComponent<Rigidbody>();
        wheelCollider = GetComponent<Collider>();
        wheelTransform = GetComponent<Transform>();
        gameController = GameObject.FindGameObjectsWithTag("GameController");
    }
    void FixedUpdate()
    {


    }
    // Update is called once per frame
    void Update()
    {
        if (checkPoints == 6)
        {
            EndOfGame();
        }
        if (this.transform.rotation.eulerAngles.z >= 1 && this.transform.rotation.eulerAngles.z <= 59)
        {
            if (checkPoints == 0)
            {
                checkPoints++;
            }
            else if (checkPoints == 2)
            {
                checkPoints--;
            }
        }
        else if (this.transform.rotation.eulerAngles.z >= 60 && this.transform.rotation.eulerAngles.z <= 119)
        {
            if (checkPoints == 1)
            {
                checkPoints++;
            }
            else if (checkPoints == 3)
            {
                checkPoints--;
            }
        }
        else if (this.transform.rotation.eulerAngles.z >= 120 && this.transform.rotation.eulerAngles.z <= 179)
        {
            if (checkPoints == 2)
            {
                checkPoints++;
            }
            else if (checkPoints == 4)
            {
                checkPoints--;
            }
        }
        else if (this.transform.rotation.eulerAngles.z >= 180 && this.transform.rotation.eulerAngles.z <= 239)
        {
            if (checkPoints == 3)
            {
                checkPoints++;
            }
            else if (checkPoints == 5)
            {
                checkPoints--;
            }
        }
        else if (this.transform.rotation.eulerAngles.z >= 240 && this.transform.rotation.eulerAngles.z <= 299)
        {
            if (checkPoints == 4)
            {
                checkPoints++;
            }
            else if (checkPoints == 6)
            {
                checkPoints--;
            }
        }
        else if (this.transform.rotation.eulerAngles.z >= 300 && this.transform.rotation.eulerAngles.z <= 359)
        {
            if (checkPoints == 5)
            {
                checkPoints++;
            }

        }
       
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
        }
        else if (!isGrabbing && !isSlide)
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

                    ray = new Ray(test.transform.position, gameController[0].transform.position - test.transform.position);
                    rayo1 = (ray.direction);
                    Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);
                }
                if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
                {
                    Ray ray2 = new Ray(test.transform.position, (gameController[0].transform.position - test.transform.position));
                    rayo2 = (ray2.direction);
                    Physics.Raycast(ray2.origin, ray2.direction);
                    Debug.DrawRay(test.transform.position, (gameController[0].transform.position - test.transform.position), Color.blue);
                    angle = Vector3.SignedAngle(rayo1, rayo2, Vector3.back);

                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (wheelCurrentPosition - angle)));

                }
                if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                {
                    wheelCurrentPosition = this.transform.rotation.eulerAngles.z;
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

    void EndOfGame()
    {
         #if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

#else 
        Application.Quit();
#endif
    }
}
