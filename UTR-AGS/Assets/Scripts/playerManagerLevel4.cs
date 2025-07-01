using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class playerManagerLevel4 : MonoBehaviour
{
    cameraViewManager cameraView;
    listManager list;
    [SerializeField]bool canTakePhoto=true;
    float speed = 0f;
    [SerializeField] Image panel;
    public float timer = 0f;
    AudioSource soundPlayer;
    bool allowToPlay=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        canTakePhoto = true;
    }
    void Start()
    {
        //Get objects
        cameraView = GameObject.Find("CameraView").GetComponent<cameraViewManager>();
        list= GameObject.Find("GameManager").GetComponent<listManager>();
        soundPlayer = GetComponentInChildren<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //If you can take a photo the timer is 0
        if (canTakePhoto) {
            timer = 0f;
        }

        if (allowToPlay)
        {
            soundPlayer.PlayOneShot(soundPlayer.clip, 1f);
            allowToPlay = false;
        }

        //effecto for the camera flash

        if (canTakePhoto==false)
        {
            //play sound when photo taken once
            
                
            //set panel alfa to 0% graddualy
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.g, Mathf.SmoothDamp(panel.color.a, 0, ref speed, 0.2f));
            turnOnVariable();
        }
        //If you can take a photo
        if (canTakePhoto == true)
        {
            //You can press any of this 3 buttons
            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                //You cant take a photo
                canTakePhoto = false;
                allowToPlay = true;

                //set panel alfa to 100% 
                panel.color = new Color(panel.color.r, panel.color.b, panel.color.g, 255f);
                

                if (cameraView.objectInView != null)
                {
                    //Get the item in the camera view
                    if (cameraView.objectInView.GetComponent<modelManager>().isImportant == true)
                    {
                        //Deactivate the objecte
                        cameraView.objectInView.gameObject.SetActive(false);
                        //Delete it from the list
                        list.peopleToPhotograph[list.numberOfObject].GetComponent<modelManager>().isPhotographed = true;
                        


                    }
                }
                
                
            }
        }
    }
    //Change var state after 2 seconds
    void turnOnVariable()
    {
        
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            canTakePhoto = true;
        }
    }
}
