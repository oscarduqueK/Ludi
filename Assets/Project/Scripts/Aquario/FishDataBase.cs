using System.Collections.Generic;
using UnityEngine;

public class FishDatabase : MonoBehaviour
{
    public static FishDatabase Instance;

    public List<FishData> fishes;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializa la lista si está vacía
        if (fishes == null)
            fishes = new List<FishData>();

        Debug.Log($"FishDatabase inicializado con {fishes.Count} peces.");

        //Creación de los pescaos de los cojones:

        // Ejemplo: pez del nivel 1
        fishes.Add(new FishData { id = 0, fishName = "Paco", prefab = fish_0 });
        // referencia al prefab de Paco
    }

    public FishData GetFishDataById(int id)
    {
        return fishes.Find(f => f.id == id);
    }
    private void LoadUnlockedFishes()
    {
        foreach (var fish in fishes)
        {
            int saved = PlayerPrefs.GetInt($"Fish_Unlocked_{fish.id}", 0);
            fish.unlocked = saved == 1;
        }
    }

    public void SaveFishUnlocked(int id)
    {
        PlayerPrefs.SetInt($"Fish_Unlocked_{id}", 1);
        PlayerPrefs.Save();
        FishData fish = GetFishData(id);
        if (fish != null) fish.unlocked = true;
    }

    public FishData GetFishData(int id)
    {
        return fishes.Find(f => f.id == id);
    }
}
