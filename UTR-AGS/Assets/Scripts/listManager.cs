using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
public class listManager : MonoBehaviour
{
    public GameObject[] peopleToPhotograph;//Array of father Objects
    public List<GameObject> activeItems = new List<GameObject>();
    [SerializeField] Transform objectPosition;//Movable sd
    Vector3 spawnPosition; //Position of spawn for the items clones
    [SerializeField] float time = 0f;//timer for the spawn
    [SerializeField] float moveSpeed = 5f; // Velocidad de movimiento del objeto instanciado
    public Collider cameraView;//playerCollider
    int isReady=0;//Counts for end the scene
    bool endPasarela=false;//Counts for end the scene
    public int numberOfObject;//Random variable for spawns
    private void Awake()
    {
        //Find and store all the OG items and add them to the array
        peopleToPhotograph = GameObject.FindGameObjectsWithTag("photoModel");
    }

    void Start()
    {
        
        //Get Spawn Position
        spawnPosition = objectPosition.transform.position;
    }

    void Update()
    {
        //Timer
        time += Time.deltaTime;
        //Spawn every x Seconds
        if (time >= 3.75f)
        {
            // Instanciar el objeto
            for (int i = 0; i <= 6; i++)
            {
                //Select a random number to spawn
                numberOfObject = Random.Range(0, 6);
                //Check if the number selected is aviable (It is not photgraphed, and has not passed in the last 8 seconds)
                if (peopleToPhotograph[numberOfObject].GetComponent<modelManager>().isPhotographed == false && peopleToPhotograph[numberOfObject].GetComponent<modelManager>().justPassed == false)
                {

                    break;

                }
                else
                {
                    //if not enter in a loop until you find one that can spawn
                    i--;
                }

            }
            //Spawn the selected item and add it to the activeItems array
            activeItems.Add(Instantiate(peopleToPhotograph[numberOfObject], spawnPosition, Quaternion.identity));
            //Active the justpased var
            peopleToPhotograph[numberOfObject].GetComponent<modelManager>().startTemporalVariable();

            time = 0f;
        }
        //Move every active Item
        foreach (GameObject element in activeItems)
        {

            element.transform.position = new Vector3(
            element.transform.position.x,
            element.transform.position.y,
            element.transform.position.z - moveSpeed * Time.deltaTime);

        }

        //Check if 4 of the 6 objects were photographed
        for (int i=0,isReady=0;i<=5; i++)
        {
            if (peopleToPhotograph[i].GetComponent<modelManager>().isPhotographed == true)
            {
                isReady+=1;
            }
            if (isReady==4)
            {
                endPasarela = true;
            }
        }
        //End the game
        if (endPasarela==true)
        {
            Invoke("endGame", 1.5f);
        }

       
    }
    void endGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
                        Application.Quit();
#endif
    }
}