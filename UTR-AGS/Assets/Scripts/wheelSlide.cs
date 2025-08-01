using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class wheelSlide : MonoBehaviour
{
    Rigidbody wheelRigidbody;
    BoxCollider wheelCollider;
    Transform wheelTransform;
    public float slideTop, slideBottom, threshold;
    public bool isSlide, isImageComplete, isGrabbing, playSoundWheel,playPaperSound = false,startAnimation=false;
    public GameObject test;
    public GameObject[] gameController;
    Vector3 rayo1, rayo2;
    [SerializeField] float wheelCurrentPosition = 0;
    public float angle, time=0;
    [SerializeField] int checkPoints = 0;
    changeState changeStateScript;
    AudioSource audioSource, chageAudioSource, BGMAudioSource, paperSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       paperSound = GameObject.Find("FullImage").GetComponent<AudioSource>();
    }
    void Start()
    {
         changeStateScript = GameObject.Find("ObjectDetector").GetComponent<changeState>();
        test = GameObject.Find("wheelPosition");
        wheelRigidbody = GetComponent<Rigidbody>();
        wheelCollider = GetComponent<BoxCollider>();
        wheelTransform = GetComponent<Transform>();
        gameController = GameObject.FindGameObjectsWithTag("GameController");
        audioSource = GetComponent<AudioSource>();
        chageAudioSource = GameObject.Find("ChangeBGM").GetComponent<AudioSource>();
        BGMAudioSource = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        
    }
    void FixedUpdate()
    {


    }
    // Update is called once per frame
    void Update()
    {
        isImageComplete = changeStateScript.imageComplete;

        if (playSoundWheel)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        if (playPaperSound)
        {
            if (paperSound.isPlaying == false)
            {
                paperSound.Play();
            }
        }
        else
        {
            if (paperSound.isPlaying)
            {
                paperSound.Stop();
            }
            
        }



        if (checkPoints == 6)
        {
           startAnimation = true;
            
            
        }
        if (startAnimation)
        {
             moveImage();
            if (BGMAudioSource.isPlaying)
            {
                BGMAudioSource.Stop();
            }
            if(chageAudioSource.isPlaying == false)
            {
                chageAudioSource.Play();
            }
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
            wheelCollider.size = new Vector3(0.0144739803f, 0.0142356977f, 0.0038292231f);
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
        if (isImageComplete)
        {
            if (!isSlide)
            {
                if (other.gameObject.CompareTag("GameController"))
                {

                    if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
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
                    if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
                    {
                        playSoundWheel = true;
                        
                        ray = new Ray(test.transform.position, gameController[0].transform.position - test.transform.position);
                        rayo1 = (ray.direction);
                        Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);
                    }
                    if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)&&OVRInput.Get(OVRInput.RawButton.LHandTrigger))
                    {
                        
                        Ray ray2 = new Ray(test.transform.position, (gameController[0].transform.position - test.transform.position));
                        rayo2 = (ray2.direction);
                        Physics.Raycast(ray2.origin, ray2.direction);
                        Debug.DrawRay(test.transform.position, (gameController[0].transform.position - test.transform.position), Color.blue);
                        angle = Vector3.SignedAngle(rayo1, rayo2, Vector3.back);

                        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (wheelCurrentPosition - angle)));

                    }
                    if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
                    {
                        wheelCurrentPosition = this.transform.rotation.eulerAngles.z;
                        playSoundWheel = false;
                    }

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
    void moveImage()
    {
        if (time <= 3.2f)
        {


            playPaperSound = true;


            changeStateScript.fullImage.transform.position = new Vector3(changeStateScript.fullImage.transform.position.x - 0.5f * Time.deltaTime, changeStateScript.fullImage.transform.position.y, changeStateScript.fullImage.transform.position.z);
            time += Time.deltaTime;
        }
        else
        {
            playPaperSound = false;
            Invoke("EndOfGame", 2f);
            
        }
        
        
       
        
        
    }
}
