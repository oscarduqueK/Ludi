using Unity.VisualScripting;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    public Aquarium aquarium;

    public bool UnlockSequence(int fishId)
    {
        if (aquarium == null)
        {
            return false;
        }

        if (aquarium.IsUnlocked(fishId))
        {
            return false;
        }

        bool unlockedNow = aquarium.SetUnlocked(fishId);
        return unlockedNow;
    }
}
