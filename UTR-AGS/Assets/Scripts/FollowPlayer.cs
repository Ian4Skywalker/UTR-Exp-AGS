using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("References")]
    public Transform personaTransform;
    public Transform headTransform;
    public Transform playerCamera;

    [Header("Head Tracking Settings")]
    public float rotationSpeed = 5f;
    public float maxHeadTurnAngle = 90f;

    [Header("Gaze Activation Timing")]
    public float attentionDelay = 0.4f;

    [Header("Idle Head Motion")]
    public float idleWiggleSpeed = 1f;
    public float idleWiggleAmount = 0.3f;

    private Quaternion originalHeadRotation;
    private float attentionTimer = 0f;
    private bool isLookingAtPlayer = false;

    void Start()
    {
        if (headTransform != null)
            originalHeadRotation = headTransform.localRotation;
    }

    void LateUpdate()
    {
        if (playerCamera == null || headTransform == null || personaTransform == null) return;

        Vector3 fullDirectionToPlayer = playerCamera.position - headTransform.position;
        Vector3 flatDirection = fullDirectionToPlayer; flatDirection.y = 0;
        if (flatDirection.sqrMagnitude < 0.01f) return;

        float angleToPlayer = Vector3.Angle(personaTransform.forward, flatDirection);

        if (angleToPlayer <= maxHeadTurnAngle)
        {
            attentionTimer += Time.deltaTime;
            if (attentionTimer >= attentionDelay) isLookingAtPlayer = true;
        }
        else
        {
            isLookingAtPlayer = false;
            attentionTimer = 0f;
        }

        if (isLookingAtPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(fullDirectionToPlayer.normalized, Vector3.up);
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion idleWiggle = Quaternion.Euler(
                Mathf.Sin(Time.time * idleWiggleSpeed) * idleWiggleAmount,
                0f,
                0f
            );
            Quaternion targetRotation = personaTransform.rotation * originalHeadRotation * idleWiggle;
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
