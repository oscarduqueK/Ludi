using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    // Nuevo evento
    public static event Action OnFishChanged;

    void Start()
    {
        if (FishDatabase.Instance == null || FishDatabase.Instance.fishes.Count == 0)
        {
            Debug.LogError("FishDatabase no inicializado o lista vacía");
            return;
        }

        if (infoText == null)
            infoText = FindAnyObjectByType<TextMeshProUGUI>();

        allFish = FishDatabase.Instance.fishes;
        currentIndex = 0;
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
        if (allFish == null || allFish.Count == 0)
        {
            Debug.LogWarning("No hay peces para mostrar.");
            return;
        }

        if (currentFishInstance != null)
            Destroy(currentFishInstance);

        index = Mathf.Clamp(index, 0, allFish.Count - 1);
        currentIndex = index;

        FishData data = allFish[index];
        bool isUnlocked = data.unlocked;

        if (!isUnlocked)
        {
            //infoText.text = "???";
            Debug.Log($"Pez {data.fishName} bloqueado.");
        }
        else
        {
            infoText.gameObject.SetActive(false);
        }

        if (data.prefab == null)
        {
            Debug.LogWarning($"Fish {data.fishName} no tiene prefab asignado.");
            return;
        }

        currentFishInstance = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);

        Fish fishLogic = currentFishInstance.GetComponent<Fish>();
        if (fishLogic != null)
        {
            fishLogic.Initialize(data);
            fishLogic.OnSpawn();
        }

        // Dispara el evento cada vez que se cambia de pez
        OnFishChanged?.Invoke();
    }

    public FishData GetCurrentFishData()
    {
        if (allFish == null || allFish.Count == 0) return null;
        currentIndex = Mathf.Clamp(currentIndex, 0, allFish.Count - 1);
        return allFish[currentIndex];
    }

    public GameObject GetCurrentFishInstance()
    {
        return currentFishInstance;
    }
}
