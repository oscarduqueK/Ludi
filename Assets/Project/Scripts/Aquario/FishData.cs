using UnityEngine;

[System.Serializable]
public class FishData
{
    public int id;                   
    public string fishName;          
    public GameObject prefab;         
    [TextArea] public string description;
    public bool unlocked = false;     
}

