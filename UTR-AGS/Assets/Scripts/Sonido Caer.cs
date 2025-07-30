using UnityEngine;

public class SonidoCaer : MonoBehaviour
{
    public AudioClip sonidoCaida;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (sonidoCaida != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sonidoCaida);
        }
    }
}
