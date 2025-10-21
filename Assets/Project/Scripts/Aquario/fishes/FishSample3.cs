using UnityEngine;
using UnityEngine.UI;

public class FishSample3 : Fish
{
    public override GameObject GetPrefab()
    {
        return Resources.Load<GameObject>("Prefabs/Fish3");

    }

    public override void SetupInfo()
    {
        Text infoText = infoPanel.GetComponentInChildren<Text>();
        if (infoText != null)
            infoText.text = "Pez 3 — Raro, tropical, muy activo bajo el agua 🌊";
    }
}
