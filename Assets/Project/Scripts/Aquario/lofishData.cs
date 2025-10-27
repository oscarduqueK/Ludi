using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loFishData : MonoBehaviour
{
    public int id;
    public string fishName;
    public GameObject prefab;
    [TextArea] public string description;
    public bool unlocked = false;
}
