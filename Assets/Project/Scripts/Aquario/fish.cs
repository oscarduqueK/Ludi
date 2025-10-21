using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class Fish : MonoBehaviour
{
    public virtual void ShowFish()
    {
        Debug.Log("a");
    }

    public virtual void FishInfo()
    {
        Debug.Log("e");
    }
}

