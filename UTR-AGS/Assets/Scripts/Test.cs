using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Audio;

public class Test : MonoBehaviour
{

    public EjectionSequence ejectionSequence;
    public GameObject persona;
    private Grabbable grababble;

    private void Start()
    {
        grababble = persona.GetComponentInChildren<Grabbable>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!grababble._isKinematicLocked)
        {
            ejectionSequence.StartEjection();
        }
    }
}
