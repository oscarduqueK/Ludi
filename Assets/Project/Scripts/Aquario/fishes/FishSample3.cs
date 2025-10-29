using UnityEngine;
using TMPro;

public class FishSample3 : Fish
{
    public override string GetInfoString()
    {
        if (!isFishUnlocked) // field heredado
            return "???";

        // Texto personalizado para este pez
        return "Nom: Tonyina\nEdat: 20 anys.";
    }

    public void ViewInfo(TextMeshProUGUI infoText)
    {
        if (infoText == null) return;
        infoText.text = GetInfoString();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        if (isFishUnlocked)
        {
            Debug.Log($"{fishName} ha aparecido en el acuario (desbloqueado).");
            if (animator != null)
                animator.Play("AtunIdle");
        }
    }

    public override void OnUnlock()
    {
        base.OnUnlock();

        isFishUnlocked = true;

        Debug.Log($"{fishName} ha sido desbloqueado.");
    }
}
