using UnityEngine;

public class pieceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    enum statues
    {
        statue1, statue2, statue3,statue4
    }
   
    [SerializeField] statues statue;
    public int piecePosition=0;
    public bool alreadyCounted=false;
    public string getStatue()
    {
        Debug.Log("Testeando"+statue.ToString());
        return statue.ToString();
    }
}
