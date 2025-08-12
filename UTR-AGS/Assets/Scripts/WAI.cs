using UnityEngine;
/// <summary>
/// This script randomly assigns a texture to the object's material when the game starts.
/// It loads predefined textures from the Resources folder and selects one randomly.
/// </summary>
public class WAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Create an array containing textures loaded from the Resources folder
        Texture[] textures = new Texture[]
        {
            Resources.Load<Texture>("coco"),  // Load first texture named "coco"
            Resources.Load<Texture>("coco2"), // Load second texture named "coco2"
            Resources.Load<Texture>("coco3")  // Load second texture named "coco3"
        };

        // Generate a random index to select a texture
        int randomIndex = Random.Range(0, textures.Length);

        // Assign the randomly selected texture to the object's material
        GetComponent<Renderer>().material.mainTexture = textures[randomIndex];
    }
}
