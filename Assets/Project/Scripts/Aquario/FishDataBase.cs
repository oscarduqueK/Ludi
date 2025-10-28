using System.Collections.Generic;
using UnityEngine;

public class FishDatabase : MonoBehaviour
{
    public static FishDatabase Instance;

    [Header("Prefabs de peces (asignar desde el Inspector)")]
    public List<GameObject> fishPrefabs = new List<GameObject>();

    [HideInInspector]
    public List<FishData> fishes = new List<FishData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (fishPrefabs == null || fishPrefabs.Count == 0)
                Debug.LogWarning("FishDatabase: No hay prefabs asignados!");
            else
                InitializeFishData(); // aquí se llenan los FishData
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFishData()
    {
        fishes.Clear();

        for (int i = 0; i < fishPrefabs.Count; i++)
        {
            FishData newFish = new FishData
            {
                id = i,
                fishName = $"Fish_{i}",
                prefab = fishPrefabs[i],
                unlocked = PlayerPrefs.GetInt($"Fish_Unlocked_{i}", 0) == 1
            };

            fishes.Add(newFish);
        }

        Debug.Log($"[FishDatabase] Inicializados {fishes.Count} peces.");
    }

    public FishData GetFishDataById(int id)
    {
        return fishes.Find(f => f.id == id);
    }

    public FishData GetFishData(int id)
    {
        return fishes.Find(f => f.id == id);
    }

    public void SaveFishUnlocked(int id)
    {
        PlayerPrefs.SetInt($"Fish_Unlocked_{id}", 1);
        PlayerPrefs.Save();

        FishData fish = GetFishData(id);
        if (fish != null)
        {
            fish.unlocked = true;
            Debug.Log($"[FishDatabase] Pez {id} guardado como desbloqueado.");
        }
    }

    private void LoadUnlockedFishes()
    {
        foreach (var fish in fishes)
        {
            fish.unlocked = PlayerPrefs.GetInt($"Fish_Unlocked_{fish.id}", 0) == 1;
        }
    }
}
