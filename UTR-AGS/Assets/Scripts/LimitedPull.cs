using UnityEngine;
using Meta.XR.BuildingBlocks;
using Oculus.Interaction;

public class LimitedPull : MonoBehaviour
{
    private InteractableUnityEventWrapper interactable;
    private bool isGrabbed = false;

    [Header("Z Axis Constraints")]
    public float minZ = 0.005f; // 0.017 - 0.012
    public float maxZ = 0.017f; // posición inicial

    void Start()
    {
        interactable = GetComponent<InteractableUnityEventWrapper>();
        if (interactable != null)
        {
            interactable.WhenSelect.AddListener(() => isGrabbed = true);
            interactable.WhenUnselect.AddListener(() => isGrabbed = false);
        }

        transform.localPosition = new Vector3(0f, 0f, maxZ);
    }

    void Update()
    {
        if (isGrabbed)
        {
            Vector3 localPos = transform.localPosition;

            // Solo permitir movimiento en Z
            localPos.x = 0f;
            localPos.y = 0f;
            localPos.z = Mathf.Clamp(localPos.z, minZ, maxZ);

            transform.localPosition = localPos;
        }
        else
        {
            // Si no está agarrado, asegúrate que X e Y sigan en 0
            transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
        }
    }
}
