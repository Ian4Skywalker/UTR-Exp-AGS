using UnityEngine;

public class DetectordeObjetos : MonoBehaviour
{
    
    public GameObject partes;
    
    public bool imageComplete = false;
    changeState changeStateScript;
    bool alreadyCounted = false;
    
    void Start()
    {
        changeStateScript = GameObject.Find("ObjectDetector").GetComponent<changeState>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        // Validación: que aún haya objetos por activar


        // Validación: que el objeto que colisiona sea el esperado
        if (other.CompareTag("Object"))
        {
            
            Debug.Log("Objeto detectado: " + other.name);

            // Desactivar el objeto que colisiona
            // other.gameObject.SetActive(false);


            partes.SetActive(true);
            Debug.Log("Objeto activado: " + partes);
            if(!alreadyCounted)
            {
                addCount();
            }
            Debug.Log("Objeto destruido: " + this.gameObject);
            Destroy(this.gameObject);

        }



    }
    void addCount()
    {
        changeStateScript.AddCount();
        alreadyCounted = true;
        
    }
}

