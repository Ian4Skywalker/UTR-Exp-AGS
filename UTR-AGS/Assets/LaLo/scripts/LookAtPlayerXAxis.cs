using UnityEngine;

public class LookAtTargetXAxis : MonoBehaviour
{
    [Tooltip("Objeto al que este objeto rotar� su eje X")]
    public Transform objetivo;

    [Tooltip("Frente personalizado del objeto. Usa esto si el modelo no mira hacia adelante por defecto.")]
    public Vector3 frentePersonalizado = Vector3.up;

    [Tooltip("Velocidad de suavizado de la rotaci�n")]
    public float smoothSpeed = 5f;

    void Update()
    void Update()
    {
        if (objetivo == null) return;

        // Calcula la direcci�n local al objetivo
        Vector3 direccionLocal = transform.InverseTransformPoint(objetivo.position);

        // Calcula el �ngulo en X necesario para mirar al objetivo
        float anguloX = Mathf.Atan2(direccionLocal.y, direccionLocal.z) * Mathf.Rad2Deg;

        // Obtiene la rotaci�n actual en euler
        Vector3 eulerActual = transform.localEulerAngles;

        // Crea la rotaci�n deseada solo en el eje X
        Quaternion rotacionDeseada = Quaternion.Euler(anguloX, eulerActual.y, eulerActual.z);

        // Suaviza la rotaci�n usando Lerp
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacionDeseada, Time.deltaTime * smoothSpeed);
    }
}
