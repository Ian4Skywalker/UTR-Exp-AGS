using System.Collections;
using UnityEngine;

public class AnimacionSecuencial : MonoBehaviour
{
    public Animator Pieza1;
    public Animator Pieza2;
    public Animator Pieza3;
    public Animator Pieza4;

    public float duracionAnimacion = 2.0f; // Duración de cada animación en segundos

    void Start()
    {
        StartCoroutine(EjecutarAnimaciones());
    }

    IEnumerator EjecutarAnimaciones()
    {
        // Orden personalizado: Cubo1 → Cubo3 → Cubo2 → Cubo4

        Pieza1.SetTrigger("Play");
        yield return new WaitForSeconds(duracionAnimacion);

        Pieza3.SetTrigger("Play");
        yield return new WaitForSeconds(duracionAnimacion);

        Pieza2.SetTrigger("Play");
        yield return new WaitForSeconds(duracionAnimacion);

        Pieza4.SetTrigger("Play");
        yield return new WaitForSeconds(duracionAnimacion);
    }
}

