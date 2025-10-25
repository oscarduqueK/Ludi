using Unity.VisualScripting;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    public bool UnlockSequence(int fishId)
    {
        if (Aquarium.Instance == null)
        {
            // Se asegura de que Aquarium esté instanciado automáticamente
            new GameObject("Aquarium").AddComponent<Aquarium>();
        }

        if (Aquarium.Instance.IsUnlocked(fishId))
        {
            Debug.Log($"fishUnlockement: Pez {fishId} ya estaba desbloqueado.");
            return false;
        }

        bool unlockedNow = Aquarium.Instance.SetUnlocked(fishId);
        Debug.Log($"fishUnlockement: Pez {fishId} desbloqueado ahora = {unlockedNow}");
        return unlockedNow;
    }
}
