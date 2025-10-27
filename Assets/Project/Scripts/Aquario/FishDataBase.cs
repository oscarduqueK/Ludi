using System.Collections.Generic;
using UnityEngine;

public class FishDatabase : MonoBehaviour
{
    public static FishDatabase Instance;

    [Header("Lista de todos los peces disponibles")]
    public List<FishData> fishes = new List<FishData>();

    void Awake()
    {
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

        LoadUnlockedFishes();
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
