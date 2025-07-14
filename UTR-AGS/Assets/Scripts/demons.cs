using Oculus.Interaction;
using UnityEngine;

public class demons : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject rat;
    private Grabbable grababble;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grababble = rat.GetComponentInChildren<Grabbable>();
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
