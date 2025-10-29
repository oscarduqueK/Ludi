using UnityEngine;
using TMPro;

public class FishSample4 : Fish
{
    public override string GetInfoString()
    {
        if (!isFishUnlocked) // field heredado
            return "";

        // Texto personalizado para este pez
        return "Nom: Verat\n" +
            "Nom cientific: Scomber scombrus\n" +
            "Habitat: Atlantic i Mediterrani\n" +
            "Pes: 750 grams\n" +
            "Mida: 35 cm\n";
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
                animator.Play("bacallaIdle");
        }
    }

    public override void OnUnlock()
    {
        base.OnUnlock();

        isFishUnlocked = true;

        Debug.Log($"{fishName} ha sido desbloqueado.");
    }
}
