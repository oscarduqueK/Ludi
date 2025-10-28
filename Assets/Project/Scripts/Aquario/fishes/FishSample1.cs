using UnityEngine;
using TMPro;

public class FishSample1 : Fish
{
   
    public bool isFishUnlocked = false;

    public string GetInfoString()
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
