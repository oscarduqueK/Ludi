using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    public bool UnlockSequence(int fishId)
    {
        // Asegurar que el Aquarium existe
        if (Aquarium.Instance == null)
        {
            Aquarium existing = FindAnyObjectByType<Aquarium>();
            if (existing != null)
            {
                Aquarium.Instance = existing;
                Debug.Log("[fishUnlockement] Se ha encontrado un Aquarium existente en la escena.");
            }
            else
            {
                GameObject aquariumGO = new GameObject("Aquarium");
                Aquarium.Instance = aquariumGO.AddComponent<Aquarium>();
                DontDestroyOnLoad(aquariumGO);
                Debug.Log("[fishUnlockement] Nuevo Aquarium creado dinámicamente.");
            }
        }

        // Inicializar UI y contenedor si hace falta
        Aquarium.Instance.InitializeIfNeeded();

        // Buscar el prefab del pez en la base de datos
        FishData data = Aquarium.Instance.allFishDatabase.Find(f => f.id == fishId);
        if (data == null || data.prefab == null)
        {
            Debug.LogWarning($"[fishUnlockement] No se encontró prefab para el pez ID={fishId}");
            return false;
        }

        // Comprobar si ya estaba desbloqueado
        if (Aquarium.Instance.unlockedFishPrefabs.Contains(data.prefab))
        {
            Debug.Log($"[fishUnlockement] Pez {fishId} ya estaba desbloqueado.");
            return false;
        }

        // Desbloquear y refrescar acuario
        Aquarium.Instance.UnlockFish(data.prefab);
        Aquarium.Instance.RefreshAquarium();
        Debug.Log($"[fishUnlockement] Pez {fishId} desbloqueado.");

        return true;
    }
}
