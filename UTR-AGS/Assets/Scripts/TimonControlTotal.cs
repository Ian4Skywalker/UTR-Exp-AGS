using UnityEngine;

public class TimonControTotal : MonoBehaviour
{
    [Header("Movimiento manual en Y")]
    public float yMin = -0.5f;
    public float yMax = 0f;

    [Header("Rotación manual limitada")]
    public float anguloMinimoZ = 0f;
    public float anguloMaximoZ = 180f;
    public float tolerancia = 5f;

    private Rigidbody rb;
    private float ultimaY;
    private bool bajadoCompleto = false;
    private bool giroCompleto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ultimaY = transform.localPosition.y;

        if (rb == null)
        {
            Debug.LogWarning("❌ El timón necesita un Rigidbody para movimiento físico.");
        }
        else
        {
            rb.isKinematic = true;      // ✅ Evita simulación física al inicio
            rb.useGravity = false;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // 🧯 Detener movimiento físico no deseado
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 pos = transform.localPosition;

        // 🔽 Solo permitir movimiento descendente en Y
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        if (pos.y > ultimaY)
        {
            pos.y = ultimaY;
        }

        // 🧭 Bloquear movimiento en X y Z
        pos.x = 0f;
        pos.z = 0f;
        transform.localPosition = pos;
        ultimaY = pos.y;

        // ✅ Detectar si llegó al fondo
        if (!bajadoCompleto && Mathf.Approximately(pos.y, yMin))
        {
            bajadoCompleto = true;
            Debug.Log("🔓 El timón está completamente bajado. El giro está habilitado.");
        }

        // 🔒 Bloquear rotación fuera de rango Z
        Vector3 rot = transform.localEulerAngles;
        float rotZ = rot.z;
        rotZ = (rotZ > 180) ? rotZ - 360 : rotZ; // convertir a -180 a 180
        float rotZLimitado = Mathf.Clamp(rotZ, anguloMinimoZ, anguloMaximoZ);
        transform.localEulerAngles = new Vector3(rot.x, rot.y, rotZLimitado);

        // 🎯 Detectar giro completo
        if (bajadoCompleto && !giroCompleto && Mathf.Abs(rotZLimitado - anguloMaximoZ) < tolerancia)
        {
            giroCompleto = true;
            AccionFinal();
        }
    }

    void AccionFinal()
    {
        // 🔓 Activar física real solo después del giro completo
        rb.isKinematic = false;
        rb.useGravity = true;

        Debug.Log("✅ ¡Acción completada! El timón fue bajado y girado correctamente.");
        // Aquí puedes activar puertas, sonidos, animaciones, etc.
    }
}
