using System.Collections.Generic;
using UnityEngine;

public class fishUnlockement : MonoBehaviour
{
    public static fishUnlockement Instance;

    [Header("Referencia a la base de datos de peces")]
    public FishDatabase fishDatabase;

    [HideInInspector]
    public int lastUnlockedId = -1; 

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Si no está asignada la base de datos, intenta encontrarla
        if (fishDatabase == null)
        {
            fishDatabase = FindAnyObjectByType<FishDatabase>();
            if (fishDatabase == null)
                Debug.LogError("No se encontró ningún FishDatabase en la escena.");
        }
    }

    /// <summary>
    /// Intenta desbloquear un pez según su ID.
    /// Devuelve true si se ha desbloqueado por primera vez, false si ya estaba desbloqueado.
    /// </summary>
    public bool UnlockSequence(int fishId)
    {
        if (fishDatabase == null)
        {
            Debug.LogWarning("No hay referencia a FishDatabase.");
            return false;
        }

        FishData fish = fishDatabase.GetFishData(fishId);

        if (fish == null)
        {
            Debug.LogWarning($"No se encontró FishData con id {fishId}");
            return false;
        }

        // Si ya estaba desbloqueado, no hacemos nada
        if (fish.unlocked)
        {
            Debug.Log($"El pez {fish.fishName} ya estaba desbloqueado.");
            return false;
        }

        // Marcarlo como desbloqueado
        fish.unlocked = true;
        PlayerPrefs.SetInt($"Fish_Unlocked_{fish.id}", 1);
        PlayerPrefs.Save();

        // Actualizar base de datos (por si la usa otra escena)
        fishDatabase.SaveFishUnlocked(fishId);

        lastUnlockedId = fishId;

        Debug.Log($"¡Nuevo pez desbloqueado! ID: {fishId} → {fish.fishName}");
        return true;
    }

    /// <summary>
    /// Devuelve si un pez está desbloqueado.
    /// </summary>
    public bool IsUnlocked(int fishId)
    {
        if (fishDatabase == null) return false;

        FishData fish = fishDatabase.GetFishData(fishId);
        return fish != null && fish.unlocked;
    }

    /// <summary>
    /// Devuelve todos los peces (útil para el Aquarium).
    /// </summary>
    public List<FishData> GetAllFishes()
    {
        if (fishDatabase == null) return new List<FishData>();
        return fishDatabase.fishes;
    }
}
