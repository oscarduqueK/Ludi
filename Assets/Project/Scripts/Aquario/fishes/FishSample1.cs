using UnityEngine;
using UnityEngine.UI;

public class FishSample1 : Fish
{
    public new bool isFishUnlocked = false;
    public override void ViewInfo(Aquarium aquarium)
    {
        aquarium.infoText.text = "Paco";
    }
}
