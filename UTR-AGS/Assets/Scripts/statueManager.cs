using Meta.WitAi;
using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class statueManager : MonoBehaviour
{
    enum statues
    {
        statue1, statue2, statue3,statue4
    }
    [SerializeField] statues statue;
    public Transform[] puzzlePosition;
    [SerializeField] GameObject objectToPiece;
    [SerializeField]int piecePosition;
    int pieceCount=0;
    List<GameObject> childPieces = new List<GameObject>();
    spawnManager Manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Manager = GameObject.Find("GameManager").GetComponent<spawnManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToPiece != null) {
            if (objectToPiece.CompareTag("piece"))
            {
                if (objectToPiece.GetComponent<pieceManager>().getStatue() == statue.ToString())
                {
                    if (objectToPiece.GetComponentInChildren<Grabbable>()._isKinematicLocked==false)
                    {
                        piecePosition = objectToPiece.GetComponent<pieceManager>().piecePosition;
                        objectToPiece.transform.position = puzzlePosition[piecePosition].position;
                        objectToPiece.transform.rotation = Quaternion.identity;
                        objectToPiece.GetComponent<Rigidbody>().isKinematic = true;
                        objectToPiece.GetComponent<Collider>().enabled = false;
                        if (objectToPiece.GetComponent<pieceManager>().alreadyCounted == false)
                        {
                            pieceCount++;
                            childPieces.Add(objectToPiece);
                            objectToPiece.GetComponent<pieceManager>().alreadyCounted = true;
                            

                        }
                    }
                    
                }


            }

        }
        if (pieceCount >= puzzlePosition.Length) {
            foreach (GameObject piece in childPieces) { 
                piece.SetActive(false);
            }
            Manager.statueCounter++;
            this.gameObject.SetActive(false);
            
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("piece"))
        {
            
            objectToPiece = other.gameObject;
        }
        
            /*objectToPiece = other.gameObject;
            if (objectToPiece.CompareTag("piece"))
            {
                if (objectToPiece.GetComponent<pieceManager>().getStatue() == statue.ToString())

                    piecePosition = objectToPiece.GetComponent<pieceManager>().piecePosition;
                objectToPiece.transform.position = puzzlePosition[piecePosition].position;
                other.GetComponent<Rigidbody>().useGravity = false;


            }*/
        
       
    }
    private void OnTriggerExit(Collider other)
    {
            if (other.CompareTag("piece"))
        {
            
            objectToPiece = null;
        }
    }
}
