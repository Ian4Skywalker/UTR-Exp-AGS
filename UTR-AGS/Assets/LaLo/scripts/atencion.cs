using UnityEngine;
using System.Collections.Generic;

public class Atencion : MonoBehaviour
{
    [SerializeField] private GameObject[] platillo_a_aparecer;
    [SerializeField] private GameObject[] aviso;
    private Vector3 initialPosition; 
    private Vector3 initialRotation;
    private int indiceActual = 0;
    private Rigidbody rb;
    public int addition;

    [Tooltip("Altura mínima antes de hacer respawn")]
    public float fallThreshold = -10f;


    
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation.eulerAngles; // Fix: Convert Quaternion to Vector3 using .eulerAngles  
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("El objeto no tiene un Rigidbody asignado.");
        }
    }

    private void Update()
    {
      
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("mesa" + (indiceActual+ addition)))
        {
            if (platillo_a_aparecer[indiceActual] != null)
            {
                platillo_a_aparecer[indiceActual].SetActive(true);
                
            }
            

            aviso[indiceActual].SetActive(false);
            // Resetear objeto para "soltarlo" y hacer respawn
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PISO"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (rb == null) return;
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation); // Fix: Use Quaternion.Euler to set rotation
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
    }
}