using Oculus.Interaction;
using UnityEngine;

public class GrabbableSound : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject objeto;
    private Grabbable grababble;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grababble = objeto.GetComponentInChildren<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grababble._isKinematicLocked)
        {
            audioSource.Play();
        }
    }
}