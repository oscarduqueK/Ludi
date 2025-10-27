using System.Collections.Generic;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    public bool UnlockSequence(int fishId)
    {
        // Paso 1: asegurar que el Aquarium existe o crearlo
        if (Aquarium.Instance == null)
        {
            var existing = FindAnyObjectByType<Aquarium>();
            if (existing != null)
            {
                Aquarium.Instance = existing;
                Debug.Log("[fishUnlockement] Se ha encontrado un Aquarium existente en la escena.");
            }
            else
            {
                // Crear uno nuevo desde cero (sin prefab)
                GameObject aquariumGO = new GameObject("Aquarium");
                var newAquarium = aquariumGO.AddComponent<Aquarium>();
                Aquarium.Instance = newAquarium;
                DontDestroyOnLoad(aquariumGO);
                Debug.Log("[fishUnlockement] Nuevo Aquarium creado dinámicamente.");
            }
        }

        // Paso 2: asegurarse de que tiene sus componentes configurados
        Aquarium.Instance.InitializeIfNeeded();

        //  Paso 3: desbloquear el pez si no lo estaba ya
        if (Aquarium.Instance.IsUnlocked(fishId))
        {
            Debug.Log($"[fishUnlockement] Pez {fishId} ya estaba desbloqueado.");
            return false;
        }

        bool unlockedNow = Aquarium.Instance.SetUnlocked(fishId);
        Debug.Log($"[fishUnlockement] Pez {fishId} desbloqueado ahora = {unlockedNow}");

        if (unlockedNow)
            Aquarium.Instance.ForceRefresh();

        return unlockedNow;
    }
}
