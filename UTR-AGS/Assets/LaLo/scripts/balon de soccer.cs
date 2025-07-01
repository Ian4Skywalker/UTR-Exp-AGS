using UnityEngine;

public class balondesoccer : MonoBehaviour
{
    [Tooltip("Objeto que se activará al colisionar con el jugador")]
    [SerializeField] private GameObject objetoAActivar;

    [Tooltip("Tag del jugador")]
    [SerializeField] private string playerTag = "Player";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameObject.SetActive(false); // Desactiva el balón
            if (objetoAActivar != null)
            {
                objetoAActivar.SetActive(true); // Activa el objeto específico
            }
        }
    }
}
