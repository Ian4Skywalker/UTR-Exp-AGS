using Oculus.Interaction;
using UnityEngine;

public class OculusGrabListener : MonoBehaviour
{
    private Grabbable grabbable;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised += HandleGrabEvent;
        }
    }

    private void HandleGrabEvent(PointerEvent evt)
    {
        switch (evt.Type)
        {
            case PointerEventType.Select:
                Debug.Log("📦 Grabbed");

                // Identifica la mano que agarra, si está disponible
                var selectingPoints = grabbable.SelectingPoints;
                foreach (Pose grabPose in selectingPoints)
                {
                    Debug.Log($"🤖 Grab pose position: {grabPose.position}");
                    Debug.Log($"🧭 Rotation: {grabPose.rotation.eulerAngles}");
                    // Aquí podrías relacionar el Pose con la mano/interactor si lo necesitas
                }
                break;

            case PointerEventType.Unselect:
                Debug.Log("👐 Released");
                break;
        }
    }

    private void OnDestroy()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= HandleGrabEvent;
        }
    }
}
