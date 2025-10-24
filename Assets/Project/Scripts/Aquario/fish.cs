using UnityEngine;

public abstract class Fish
{
    public bool isFishUnlocked = false;
    public virtual void ViewInfo(Aquarium aquarium)
    {
        Debug.Log("A");
    }
}
