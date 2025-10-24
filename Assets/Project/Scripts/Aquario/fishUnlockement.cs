using Unity.VisualScripting;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    private Fish currentFishLogic;
    private Aquarium aquarium;

    private Event PopBubble;
    private Event LoUnlockFish;

    public void UnlockSequence()
    {
        if (currentFishLogic.isFishUnlocked == false)
        {
        }
    }
}
