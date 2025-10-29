using UnityEngine;
using TMPro;

public class FishSample1 : Fish
{
    public override string GetInfoString()
    {
        if (!isFishUnlocked) // field heredado
            return "???";

        // Texto personalizado para este pez
        return "Nom: Peix Pallasso\n" +
            "Nom cientific: Amphiprioninae\n" +
            "Habitat: Ocea Indic i Pacific\n" +
            "Pes: 25 grams\n" +
            "Mida: 10 cm\n";
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
                animator.Play("nemoIdle"); 
        }

        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator != null && !animator.isActiveAndEnabled)
            animator.enabled = true;
    }

    public override void OnUnlock()
    {
        base.OnUnlock();

        isFishUnlocked = true;

        Debug.Log($"{fishName} ha sido desbloqueado.");
    }
}
