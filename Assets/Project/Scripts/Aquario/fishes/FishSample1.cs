using UnityEngine;
using UnityEngine.UI;

public class FishSample1 : Fish
{
    public override void ViewInfo(Aquarium aquarium)
    {
        aquarium.infoText.text = "Paco";
    }
}
