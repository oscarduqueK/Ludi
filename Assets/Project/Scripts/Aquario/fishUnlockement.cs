using Unity.VisualScripting;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    private Fish currentFishLogic;
    private Aquarium aquarium;

    private Event PopBubble;
    private Event LoUnlockFish;

    public Fish GetFish(int fishId)
    {
        return aquarium.AssociateFish(fishId);
    }

    public bool UnlockSequence(int fishId)
    {
        Fish currentFish = aquarium.AssociateFish(fishId);

        if (currentFish != null && !currentFish.isFishUnlocked)
        {
            currentFish.isFishUnlocked = true;
            Debug.Log("Change to unlockement secuence");

            //Pendiente por poner cosas de la animacion etc...
            return true;
        }

        return false;
    }
}
