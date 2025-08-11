using UnityEditor;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameObject[] spawnPositions;
    GameObject[] pieces;
    public GameObject doors;
    int randomNumber;
    public int statueCounter=0;
    private void Awake()
    {
        
        spawnPositions = GameObject.FindGameObjectsWithTag("spawnPosition");
        pieces = GameObject.FindGameObjectsWithTag("piece");
        for (int i = 0; i < pieces.Length; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                randomNumber = Random.Range(0, spawnPositions.Length);
                if (spawnPositions[randomNumber].GetComponent<spawnBoolean>().alreadyOcuppied == false)
                {
                    pieces[i].transform.position = new Vector3(spawnPositions[randomNumber].transform.position.x, spawnPositions[randomNumber].transform.position.y + 0.5f, spawnPositions[randomNumber].transform.position.z);
                    spawnPositions[randomNumber].GetComponent<spawnBoolean>().alreadyOcuppied = true;
                    j = 2;
                }
                else
                {
                    j--;
                }
            }
        }

    }
    void Start()
    {
        doors.SetActive(false);
    }
    void endGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
                        Application.Quit();
#endif
    }
    void openDoors()
    {
        doors.SetActive (true);
    }
    // Update is called once per frame
    void Update()
    {
        if (statueCounter >= 4) {
            openDoors();
        }

    }
}
