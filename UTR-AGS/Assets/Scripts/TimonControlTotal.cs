using UnityEngine;

public class TimonControTotal : MonoBehaviour
{
    [Header("Movimiento vertical manual")]
    public float yMin = -0.5f;
    public float yMax = 0f;

    [Header("Giro rotatorio limitado")]
    public float maxVueltas = 2f;
    public float toleranciaAnguloFinal = 5f;

    private Rigidbody rb;
    private float ultimaY;
    private bool bajadoCompleto = false;
    private bool giroCompleto = false;
    private float rotacionAcumuladaZ = 0f;
    private Vector3 ultimaRotacion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ultimaY = yMax;
        ultimaRotacion = transform.localEulerAngles;

        transform.localPosition = new Vector3(0f, yMax, 0f);

        if (rb == null)
        {
            Debug.LogWarning("❌ El timón necesita un Rigidbody.");
            return;
        }

        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        BloquearTimón();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        pos.x = 0f;
        pos.z = 0f;
        transform.localPosition = pos;
        ultimaY = pos.y;

        if (!bajadoCompleto && Mathf.Approximately(pos.y, yMin))
        {
            bajadoCompleto = true;
            Debug.Log("🔓 Timón bajado. Giro habilitado.");
        }

        if (bajadoCompleto && !giroCompleto)
        {
            Vector3 rotActual = transform.localEulerAngles;
            float deltaZ = Mathf.DeltaAngle(ultimaRotacion.z, rotActual.z);

            if (deltaZ > 0)
            {
                rotacionAcumuladaZ += deltaZ;
                Debug.Log($"↪ Vueltas acumuladas: {rotacionAcumuladaZ / 360f:F2}");
            }

            ultimaRotacion = rotActual;

            if (rotacionAcumuladaZ >= 360f * maxVueltas - toleranciaAnguloFinal)
            {
                giroCompleto = true;
                BloquearGiro();  // ✅ Bloqueo final
                AccionFinal();
            }
        }
    }

    void BloquearTimón()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY;
    }

    void BloquearGiro()
    {
        rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
    }

    void AccionFinal()
    {
        Debug.Log("✅ ¡Secuencia completada! El timón fue bajado y girado correctamente.");
        // Aquí puedes agregar efectos visuales, sonido, animaciones, etc.
    }
}
