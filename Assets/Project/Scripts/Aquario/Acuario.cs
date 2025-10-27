using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aquarium : MonoBehaviour
{
    [Header("Referencias")]
    public Transform spawnPoint;
    public TextMeshProUGUI infoText;

    [Header("Opciones")]
    public float transitionDelay = 0.2f;

    private List<FishData> allFish;
    private int currentIndex = 0;
    private GameObject currentFishInstance;

    void Start()
    {
        allFish = FishDatabase.Instance.fishes;

        if (allFish == null || allFish.Count == 0)
        {
            Debug.LogError("Aquarium: No hay peces en FishDatabase.");
            return;
        }

        ShowFish(currentIndex);
    }

    public void NextFish()
    {
        currentIndex++;
        if (currentIndex >= allFish.Count)
            currentIndex = 0;

        ShowFish(currentIndex);
    }

    public void PreviousFish()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = allFish.Count - 1;

        ShowFish(currentIndex);
    }

    private void ShowFish(int index)
    {
        if (currentFishInstance != null)
            Destroy(currentFishInstance);

        FishData data = allFish[index];

        bool isUnlocked = PlayerPrefs.GetInt($"FishUnlocked_{data.id}", 0) == 1;

        if (!isUnlocked)
        {
            // Mostrar pez bloqueado (interrogante)
            infoText.text = "???";
            Debug.Log($"Pez {data.fishName} bloqueado.");
            return;
        }

        if (data.prefab == null)
        {
            Debug.LogWarning($"Fish {data.fishName} no tiene prefab asignado.");
            return;
        }

        GameObject instance = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);
        currentFishInstance = instance;

        Fish fishLogic = instance.GetComponent<Fish>();
        if (fishLogic != null)
        {
            fishLogic.Initialize(data);
            fishLogic.OnSpawn();
        }

        infoText.text = data.fishName;
    }
}
