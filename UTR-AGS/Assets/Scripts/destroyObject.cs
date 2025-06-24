using Meta.WitAi;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    listManager listManagment;
    private void Awake()
    {
         listManagment=GameObject.Find("GameManager").GetComponent<listManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("photoModel"))
        {
            listManagment.activeItems.Remove(other.gameObject);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            
        }
    }
}