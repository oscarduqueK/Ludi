using UnityEngine;

public class FishData : MonoBehaviour
{
    public int id;                   
    public string fishName;          
    public GameObject prefab;         
    [TextArea] public string description;
    public bool unlocked = false;     
}

