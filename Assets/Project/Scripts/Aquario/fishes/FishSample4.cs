using UnityEngine;
using TMPro;

public class FishSample4 : Fish
{
    // Indica si el pez está desbloqueado (controlado por el sistema de desbloqueo)
    public bool isFishUnlocked = false;

    // Devuelve el texto que se debe mostrar en el UI para este pez.
    // Si está bloqueado devuelve "???", si no, el nombre.
    public string GetInfoString()
    {
        if (!isFishUnlocked)
            return "???";
        return fishName ?? "Unknown Fish";
    }

    // Método utilitario para que un UI pase su TextMeshProUGUI y se rellene.
    // Útil si tu Aquarium tiene el text component y quiere pedir directamente
    public void ViewInfo(TextMeshProUGUI infoText)
    {
        if (infoText == null) return;
        infoText.text = GetInfoString();
    }

    // Se llama cuando el prefab/instancia aparece en pantalla
    public override void OnSpawn()
    {
        base.OnSpawn();

        if (isFishUnlocked)
        {
            Debug.Log($"{fishName} ha aparecido en el acuario (desbloqueado).");
            if (animator != null)
                animator.SetTrigger("Idle"); // trigger opcional, pon el que tengas
        }
    }

    // Se llama cuando el sistema lo desbloquea
    public override void OnUnlock()
    {
        base.OnUnlock();

        isFishUnlocked = true;

        Debug.Log($"{fishName} ha sido desbloqueado.");
    }
}
