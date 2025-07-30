using UnityEngine;
using Meta.XR.BuildingBlocks;
using Unity.Collections;
using Oculus.Interaction;
using Unity.VisualScripting;
public class SonidoRecoger : MonoBehaviour
{
    private AudioSource audioSource;
    private Grabbable grabbable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool soundPlayed = false;
    void Start()
    {
        grabbable = this.GetComponent<Grabbable>();
        if (grabbable != null)
        {
            
        }

        audioSource = GetComponentInChildren<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (grabbable._isKinematicLocked && !soundPlayed)
        {

            ReproducirSonido();
            soundPlayed = true;
        }
        else if (!grabbable._isKinematicLocked && soundPlayed)
        {
            soundPlayed = false;
        }
       
    }

    private void ReproducirSonido()
    {
        
            Debug.Log("Reproduciendo sonido de " + audioSource.name);
            audioSource.PlayOneShot(audioSource.clip);
        
    }
}
