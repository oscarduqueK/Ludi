using UnityEngine;
using TMPro;

public class FishSample3 : Fish
{
    public override string GetInfoString()
    {
        if (!isFishUnlocked) // field heredado
            return "???";

        // Texto personalizado para este pez
        return "Nom: Tonyina\n" +
            "Nom cientific: Thunnus thynnus\n" +
            "Habitat: Atlantic i Mediterrani\n" +
            "Pes: 200 kg\n" +
            "Mida: 2 metres\n";
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
                animator.Play("atunIdle");
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
