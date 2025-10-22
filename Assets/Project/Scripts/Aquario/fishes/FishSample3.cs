using UnityEngine;
using UnityEngine.UI;

public class FishSample3 : Fish
{
    public override void ViewInfo(Aquarium aquarium)
    {
        aquarium.infoText.text = "Duro";
    }
}
