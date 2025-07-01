using UnityEngine;
using System.Collections.Generic;

public class Atencion : MonoBehaviour
{
    [SerializeField] private GameObject[] mesas;
    [SerializeField] private GameObject[] mensaje;
    [SerializeField] private GameObject[] avisos;
    [SerializeField] private GameObject[] cubierta;
    [SerializeField] private AudioSource audioSource;

    private Vector3 initialPosition; 
    private Vector3 initialRotation;
    private Vector3 aumento_izquierda = new Vector3(-0.4f, 0, 0);
    private int indiceActual = 0;
    private int indicedos = 0;
    private Rigidbody rb;
    private int activePlates = 5;
    public GameObject extra;


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
        

        if (other.CompareTag("mesa" + (indiceActual + 1)))
        {
            if (mesas[indiceActual] != null)
            {
                Renderer renderer = this.GetComponent<Renderer>();
                Renderer renderer2 = this.GetComponent<Renderer>();
                
                if (renderer != null)
                {
                    if (indicedos < cubierta.Length - 1)
                    {
                        renderer2 = cubierta[indicedos].GetComponent<Renderer>();
                        renderer.material = renderer2.material;
                    }
                    
                }
                if (activePlates > 0)
                {
                    initialPosition -= aumento_izquierda; // disminuir la posición inicial hacia abajo

                    
                    if (indiceActual < 4)
                    {
                        Destroy(cubierta[indiceActual]); // destruir la cubierta extra
                    }
                    
                   
                    activePlates--;
                }
                
                mesas[indiceActual].SetActive(true);
                
            }
            

            avisos[indiceActual].SetActive(false);
            if (indiceActual + 1 < avisos.Length && avisos[indiceActual + 1] != null)
                avisos[indiceActual + 1].SetActive(true);

            if (audioSource != null)
                audioSource.Play();

            indiceActual++;
            if (indicedos < cubierta.Length - 1)
            {
                indicedos++;
            }
            if (indiceActual == 5)
            {
                audioSource.Play();
                mensaje[0].SetActive(true);
                Destroy(this.gameObject); // Destruir el objeto al completar la tarea
            }
            // Resetear objeto para "soltarlo" y hacer respawn
            gameObject.SetActive(false);
            transform.position = initialPosition;
            gameObject.SetActive(true);
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