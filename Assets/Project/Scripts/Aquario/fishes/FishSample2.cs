using UnityEngine;
using UnityEngine.UI;

public class FishSample2 : Fish
{
    public override void ViewInfo(Aquarium aquarium)
    {
        aquarium.infoText.text = "Gerte";
    }
}
