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

    private HashSet<int> unlockedFishIds = new HashSet<int>();

    private void Awake()
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

        // Crear contenedor e infoText por código si no existen
        if (aquariumContainer == null)
        {
            GameObject containerGO = new GameObject("AquariumContainer");
            containerGO.transform.SetParent(transform);
            aquariumContainer = containerGO.transform;
        }

        if (infoText == null)
        {
            GameObject infoGO = new GameObject("InfoText");
            infoGO.transform.SetParent(transform);
            infoText = infoGO.AddComponent<TextMeshProUGUI>();
            infoText.gameObject.SetActive(false);
        }

        if (noFishMessage == null)
        {
            noFishMessage = new GameObject("NoFishMessage");
            noFishMessage.transform.SetParent(transform);
            noFishMessage.SetActive(false);
        }
    }

    void Start()
    {
        RefreshFishesFromDatabase();
        if (fishes.Count > 0) UpdateAquariumView();
        else noFishMessage.SetActive(true);
    }

    public bool IsUnlocked(int fishId) => unlockedFishIds.Contains(fishId);

    public bool SetUnlocked(int fishId)
    {
        if (IsUnlocked(fishId)) return false;
        unlockedFishIds.Add(fishId);
        return true;
    }

    private FishData GetFishData(int id) => allFishDatabase.Find(x => x.id == id);

    private void RefreshFishesFromDatabase()
    {
        fishes.Clear();
        foreach (var fd in allFishDatabase)
        {
            if (fd.unlocked && fd.prefab != null) fishes.Add(fd.prefab);
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
