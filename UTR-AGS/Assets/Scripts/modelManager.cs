using UnityEngine;

public class modelManager : MonoBehaviour
{

    public bool isImportant;
    public bool justPassed;
    public bool isPhotographed;
    //Turn justPassed True and then False after 8 secons
    public void startTemporalVariable()
    {
        justPassed = true;
        Invoke("stopTemporalVariable",8f);
    }
    void stopTemporalVariable() {
        justPassed = false; 
    }
}
