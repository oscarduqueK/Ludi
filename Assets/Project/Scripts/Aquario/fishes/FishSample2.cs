using UnityEngine;
using TMPro;

public class FishSample2 : Fish
{
    public override string GetInfoString()
    {
        if (!isFishUnlocked) // field heredado
            return "???";

        // Texto personalizado para este pez
        return "Nom: Peix Volador\n" +
            "Nom cientific: Exocoetidae\n" +
            "Habitat: Aigües calides del Tropic\n" +
            "Pes: 250 grams\n" +
            "Mida: 25 cm\n";
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
                animator.Play("flyFishIdle");
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
