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
            Debug.Log("[Aquarium] Instancia creada y marcada como persistente.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("[Aquarium] Se intentó crear una segunda instancia, destruida.");
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

        Debug.Log($"[Aquarium] Start ejecutado. Pez desbloqueados: {fishes.Count}");
        if (fishes.Count > 0) UpdateAquariumView();
        else noFishMessage.SetActive(true);
    }

    public bool IsUnlocked(int fishId) => unlockedFishIds.Contains(fishId);

    public bool SetUnlocked(int fishId)
    {
        if (IsUnlocked(fishId)) return false;

        // añade al set lógico
        unlockedFishIds.Add(fishId);

        // sincroniza el FishData si existe
        FishData fd = GetFishData(fishId);
        if (fd != null)
        {
            fd.unlocked = true;
            if (fd.prefab != null && !fishes.Contains(fd.prefab))
            {
                fishes.Add(fd.prefab);
            }
        }

        Debug.Log($"[Aquarium] SetUnlocked: id={fishId}. total unlocked={unlockedFishIds.Count}. fishes list={fishes.Count}");
        return true;
    }

    private FishData GetFishData(int id) => allFishDatabase.Find(x => x.id == id);

    public void RefreshFishesFromDatabase()
    {
        // NO borramos el HashSet: es la fuente de la verdad
        fishes.Clear();

        foreach (var fd in allFishDatabase)
        {
            if (fd == null) continue;
            if (unlockedFishIds.Contains(fd.id) && fd.prefab != null)
            {
                fd.unlocked = true;
                if (!fishes.Contains(fd.prefab))
                    fishes.Add(fd.prefab);
            }
            else
            {
                // mantener fd.unlocked coherente
                fd.unlocked = fd.unlocked && unlockedFishIds.Contains(fd.id);
            }
        }

        Debug.Log($"[Aquarium] Refrescado. fishes.Count = {fishes.Count}");
    }

    public void ForceRefresh()
    {
        RefreshFishesFromDatabase();
        // Si ya estás en la escena del acuario y quieres que se muestre inmediatamente:
        if (fishes.Count > 0)
            UpdateAquariumView();
        else
            noFishMessage?.SetActive(true);
    }

    private void OnEnable()
    {
        // Cuando el objeto se activa en la escena (o volvemos a ella), sincroniza visualmente
        RefreshFishesFromDatabase();
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
