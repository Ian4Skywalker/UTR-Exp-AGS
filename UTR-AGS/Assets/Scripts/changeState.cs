using Unity.VisualScripting;
using UnityEngine;

public class changeState : MonoBehaviour
{
    public GameObject[] objetos;
    public bool imageComplete = false;
    [SerializeField] int objectsPlaced = 0;
    public GameObject fullImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fullImage = GameObject.Find("FullImage");
        objetos = GameObject.FindGameObjectsWithTag("Object1");
    }
    void Start()
    {
        for (int obj = 0; obj < objetos.Length; obj++)
        {
            objetos[obj].SetActive(false);



        }
        fullImage.SetActive(false);


    }
    // Update is called once per frame
    void Update()
    {
        if (imageComplete)
        {
            for (int obj = 0; obj < objetos.Length; obj++)
            {
                objetos[obj].SetActive(false);



            }
            fullImage.SetActive(true);
            Debug.Log("Todos los objetos desactivados");
        }
        if (objectsPlaced >= 4)
        {
            imageComplete = true;

        }

    }
   
    public void AddCount()
    {
        objectsPlaced++;
        
    }
}
