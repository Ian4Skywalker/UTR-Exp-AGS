using UnityEngine;

public class DetectordeObjetos : MonoBehaviour
{
     public GameObject partes;

    // Índice para llevar la cuenta del objeto esperado
    
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
            Debug.Log("Objeto destruido: " + this.gameObject);
            Destroy(this.gameObject);
                 
        }



    }
}
