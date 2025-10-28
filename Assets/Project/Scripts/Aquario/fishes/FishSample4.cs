using UnityEngine;
using TMPro;

public class FishSample4 : Fish
{
    public bool isFishUnlocked = false;
    public override string GetInfoString()
    {
        if (!isFishUnlocked)
            return "???";
        return fishName ?? "Unknown Fish";
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
    }

    public override void OnUnlock()
    {
        base.OnUnlock();

        isFishUnlocked = true;

        Debug.Log($"{fishName} ha sido desbloqueado.");
    }
}
