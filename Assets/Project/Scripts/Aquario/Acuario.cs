using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static event System.Action OnFishChanged;

    void Start()
    {
        Time.timeScale = 1f;
     
        Debug.Log("TimeScale actual al entrar en Acuario: " + Time.timeScale);

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

        infoText.gameObject.SetActive(false);
        ShowFish(currentIndex);
    }

    public void PreviousFish()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = allFish.Count - 1;

        infoText.gameObject.SetActive(false);
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
        {
            Destroy(currentFishInstance);
            currentFishInstance = null;
        }

        index = Mathf.Clamp(index, 0, allFish.Count - 1);
        currentIndex = index;

        FishData data = allFish[index];
        bool isUnlocked = data.unlocked;

        if (!isUnlocked)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "???";
            Debug.Log($"Pez bloqueado: {data.fishName}");
            return;
        }

        infoText.gameObject.SetActive(false);

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

            Animator anim = fishLogic.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.enabled = true;
                anim.Play("Idle", 0, 0f);
            }
        }

        Debug.Log($"Pez mostrado: {data.fishName}");

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

    public int GetCurrentFishIndex()
    {
        return currentIndex;
    }
}
