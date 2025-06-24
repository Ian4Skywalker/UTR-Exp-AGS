using Oculus.Interaction;
using UnityEngine;

public class cubeDisapear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string cubeColor = "grabableCube";
    MeshRenderer mesh;
    [SerializeField]
        GameObject cube;

    GameObject bigCube;
    void Start()
    {
        bigCube = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cube.GetComponentInChildren<Grabbable>()._isKinematicLocked == true)
        {
            this.gameObject.SetActive(false);
        } 
        if (cube.GetComponentInChildren<Grabbable>()._isKinematicLocked == false)
        {
            bigCube.gameObject.SetActive(true);
        }
    }
}
