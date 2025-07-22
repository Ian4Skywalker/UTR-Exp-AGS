using UnityEngine;

public class TimonInteractivo : MonoBehaviour
{
    public float posicionYObjetivo = -0.5f;
    public float rotacionObjetivo = 180f;
    public float velocidadMovimiento = 0.5f;
    public float velocidadRotacion = 90f;

    private bool bajando = true;
    private bool girando = false;
    private bool completado = false;

    void Update()
    {
        if (bajando)
        {
            Vector3 nuevaPos = transform.localPosition;
            nuevaPos.y = Mathf.MoveTowards(nuevaPos.y, posicionYObjetivo, velocidadMovimiento * Time.deltaTime);
            transform.localPosition = nuevaPos;

            if (Mathf.Approximately(nuevaPos.y, posicionYObjetivo))
            {
                bajando = false;
                girando = true;
            }
        }
        else if (girando)
        {
            float nuevaRotacion = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, nuevaRotacion);

            if (Mathf.Approximately(nuevaRotacion, rotacionObjetivo))
            {
                girando = false;
                completado = true;
                AccionFinal();
            }
        }
    }

    void AccionFinal()
    {
        Debug.Log("¡El timón ha sido bajado y girado! Acción completada.");
        // Aquí puedes activar una puerta, sonido, animación, etc.
    }
}
