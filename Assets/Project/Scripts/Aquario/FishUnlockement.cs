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

        if (fishDatabase == null)
        {
            fishDatabase = FishDatabase.Instance;
            if (fishDatabase == null)
                Debug.LogWarning("No se encontró ningún FishDatabase en la escena.");
        }
    }

    public bool UnlockSequence(int fishId)
    {
        FishData data = FishDatabase.Instance.GetFishDataById(fishId);

        if (data == null)
        {
            Debug.LogWarning($"No se encontró FishData con id {fishId}");
            return false;
        }

        if (data.unlocked)
            return false;

        PlayerPrefs.SetInt($"Fish_Unlocked_{fishId}", 1);
        PlayerPrefs.Save();

        data.unlocked = true;
        lastUnlockedId = fishId;

        Debug.Log($"fishUnlockement: Pez {fishId} desbloqueado");
        return true;
    }

    public bool IsUnlocked(int fishId)
    {
        FishData fish = FishDatabase.Instance.GetFishData(fishId);
        return fish != null && fish.unlocked;
    }

    public List<FishData> GetAllFishes()
    {
        return FishDatabase.Instance != null ? FishDatabase.Instance.fishes : new List<FishData>();
    }
}
