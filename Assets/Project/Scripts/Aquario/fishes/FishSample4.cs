using UnityEngine;
using UnityEngine.UI;

public class FishSample4 : Fish
{
    public override void ViewInfo(Aquarium aquarium)
    {
        aquarium.infoText.text = "Calvo puto maricon";
    }
}

