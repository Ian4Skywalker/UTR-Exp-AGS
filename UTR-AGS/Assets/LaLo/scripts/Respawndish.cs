using UnityEngine;

public class Respawndish : MonoBehaviour
{
    private Vector3 initialPosition;  // Posici�n inicial del objeto
    public float fallThreshold = -10f; // Altura m�nima antes de hacer respawn

    void Start()
    {
        // Guardamos la posici�n inicial al comenzar
        initialPosition = transform.position;
    }
    void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        // Reseteamos la posici�n y la velocidad si tiene Rigidbody
        transform.position = initialPosition;

        
    }

    void Update()
    {
        // Si el objeto cae por debajo del umbral, lo volvemos a la posici�n inicial
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    
}
