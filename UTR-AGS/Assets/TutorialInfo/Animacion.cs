using UnityEngine;
using System.Collections.Generic;

public class Animacion : MonoBehaviour
{
    private HashSet<string> objetosColisionados = new HashSet<string>();
    private string[] objetosEsperados = { "Object 1", "Object 2", "Object 3", "Object 4" };

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró tiene un tag esperado
        foreach (string tag in objetosEsperados)
        {
            if (other.CompareTag(tag))
            {
                if (!objetosColisionados.Contains(tag))
                {
                    objetosColisionados.Add(tag);
                    Debug.Log("Colisionó con: " + tag);

                    // Opcional: destruir el objeto al colisionar
                    Destroy(other.gameObject);
                }

                // Verifica si ya han colisionado los 4 objetos
                if (objetosColisionados.Count == objetosEsperados.Length)
                {
                    ActivarAnimacion();
                }

                break; // No sigas buscando más tags
            }
        }
    }

    private void ActivarAnimacion()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Activate"); // Asegúrate de tener un Trigger llamado "Activate"
        }
        else
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }
}
