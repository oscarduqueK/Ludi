using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishData
{
    public int id;
    public string fishName;
    public GameObject prefab;
    [HideInInspector]
    public bool unlocked = false;
}

public class Aquarium : MonoBehaviour
{
    public static Aquarium Instance;
    public List<FishData> allFishDatabase = new List<FishData>();

    public List<GameObject> fishes = new List<GameObject>();

    public Transform aquariumContainer;
    public GameObject noFishMessage;
    public TextMeshProUGUI infoText;

    private int currentIndex = 0;
    private GameObject currentFishInstance;
    private Fish currentFishLogic;
    private bool isShowingInfo = false;

    void Start()
    {
        if (infoText != null)
            infoText.gameObject.SetActive(false);

        RefreshFishesFromDatabase();

        if (fishes.Count > 0)
            UpdateAquariumView();
        else
            noFishMessage.SetActive(true);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool IsUnlocked(int id)
    {
        FishData fd = GetFishData(id);
        return fd != null && fd.unlocked;
    }

    // Devuelve true si se acaba de desbloquear (primer vez)
    public bool SetUnlocked(int id)
    {
        FishData fd = GetFishData(id);
        if (fd == null) return false;
        if (fd.unlocked) return false;

        fd.unlocked = true;

        // Añadir a la lista visual si no está ya
        if (fd.prefab != null && !fishes.Contains(fd.prefab))
        {
            fishes.Add(fd.prefab);
        }

        // Actualizar vista si quieres mostrar inmediatamente
        UpdateAquariumView();

        Debug.Log($"Aquarium: fish id {id} marcado como UNLOCKED.");
        return true;
    }

    public GameObject GetPrefab(int id)
    {
        FishData fd = GetFishData(id);
        return fd != null ? fd.prefab : null;
    }

    private FishData GetFishData(int id)
    {
        return allFishDatabase.Find(x => x.id == id);
    }

    // Rellena fishes con los prefabs de los unlocked al iniciar (si quieres)
    private void RefreshFishesFromDatabase()
    {
        fishes.Clear();
        foreach (var fd in allFishDatabase)
        {
            if (fd.unlocked && fd.prefab != null)
                fishes.Add(fd.prefab);
        }
    }

    public void UnlockFish(GameObject fishPrefab)
    {
        if (!fishes.Contains(fishPrefab))
        {
            fishes.Add(fishPrefab);
            currentFishLogic.isFishUnlocked = true;
            Debug.Log($"Se ha desbloqueado el pez {fishPrefab.name}");
        }

        UpdateAquariumView();
    }

    public void NextFish()
    {
        if (fishes.Count == 0) return;
        currentIndex = (currentIndex + 1) % fishes.Count;
       
        if (isShowingInfo)
        {
            currentFishLogic = AssociateFish(currentIndex);
            if (currentFishLogic != null && infoText != null)
            {
                infoText.text = "";
                currentFishLogic.ViewInfo(this);
            }
        }
        else
        {
            UpdateAquariumView();
        }
    }

    public void PreviousFish()
    {
        if (fishes.Count == 0) return;

        currentIndex = (currentIndex - 1 + fishes.Count) % fishes.Count;
        if (isShowingInfo)
        {
            currentFishLogic = AssociateFish(currentIndex);
            if (currentFishLogic != null && infoText != null)
            {
                infoText.text = "";
                currentFishLogic.ViewInfo(this);
            }
        }
        else
        {
            UpdateAquariumView();
        }
    }

    private void UpdateAquariumView()
    {
        DestroyCurrentFish();

        if (fishes.Count == 0)
        {
            noFishMessage.SetActive(true);
            return;
        }

        noFishMessage.SetActive(false);

        if (infoText != null)
            infoText.gameObject.SetActive(false);

        currentFishInstance = Instantiate(fishes[currentIndex], aquariumContainer);
        currentFishLogic = AssociateFish(currentIndex);
        isShowingInfo = false;
    }

    public void DestroyCurrentFish()
    {
        if (currentFishInstance != null)
        {
            Destroy(currentFishInstance);
            currentFishInstance = null;
        }
    }

    public void ShowFishInfo()
    {
        DestroyCurrentFish();

        if (infoText == null)
        {
            Debug.LogWarning("infoText no está asignado en el inspector.");
            return;
        }

        infoText.gameObject.SetActive(true);
        infoText.text = ""; 

        currentFishLogic = AssociateFish(currentIndex);

        if (currentFishLogic != null)
            currentFishLogic.ViewInfo(this);

        isShowingInfo = true;
    }

    public void ShowFish()
    {
        if (currentFishInstance != null) return;

        if (fishes.Count == 0)
        {
            noFishMessage.SetActive(true);
            return;
        }

        if (infoText != null)
            infoText.gameObject.SetActive(false);

        noFishMessage.SetActive(false);

        currentFishInstance = Instantiate(fishes[currentIndex], aquariumContainer);
        currentFishLogic = AssociateFish(currentIndex);
        isShowingInfo = false;
    }

    public void ToggleFishView()
    {
        if (isShowingInfo)
            ShowFish();
        else
            ShowFishInfo();
    }

    public Fish AssociateFish(int index)
    {
        switch (index)
        {
            case 0: return new FishSample1();
            case 1: return new FishSample2();
            case 2: return new FishSample3();
            case 3: return new FishSample4();
            default: return null;
        }
    }
}
