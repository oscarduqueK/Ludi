using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishData
{
    public int id;
    public string fishName;
    public GameObject prefab;
}

public class Aquarium : MonoBehaviour
{
    public static Aquarium Instance;

    [Header("Base Data")]
    public List<FishData> allFishDatabase = new List<FishData>(); // Prefabs de todos los peces

    [Header("UI / Container")]
    public Transform aquariumContainer;
    public GameObject noFishMessage;
    public TextMeshProUGUI infoText;

    [HideInInspector]
    public List<GameObject> unlockedFishPrefabs = new List<GameObject>(); // Solo prefabs desbloqueados
    
    private int currentIndex = 0;
    private GameObject currentFishInstance;
    private Fish currentFishLogic;
    private bool isShowingInfo = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[Aquarium] Instancia creada y persistente.");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Crear contenedor y UI si no existen
        if (aquariumContainer == null)
        {
            GameObject container = new GameObject("AquariumContainer");
            container.transform.SetParent(transform);
            aquariumContainer = container.transform;
        }

        if (infoText == null)
        {
            GameObject textGO = new GameObject("InfoText");
            textGO.transform.SetParent(transform);
            infoText = textGO.AddComponent<TextMeshProUGUI>();
            infoText.gameObject.SetActive(false);
        }

        if (noFishMessage == null)
        {
            noFishMessage = new GameObject("NoFishMessage");
            noFishMessage.transform.SetParent(transform);
            var text = noFishMessage.AddComponent<TextMeshProUGUI>();
            text.text = "No fish yet!";
            noFishMessage.SetActive(false);
        }
    }

    public void Start()
    {
        RefreshAquarium();
    }

    #region Unlock / Refresh

    public void UnlockFish(GameObject fishPrefab)
    {
        if (!unlockedFishPrefabs.Contains(fishPrefab))
        {
            unlockedFishPrefabs.Add(fishPrefab);
            Debug.Log($"Se ha desbloqueado el pez {fishPrefab.name}");
        }
        RefreshAquarium(); // instancia los peces en el contenedor
    }

    public void InitializeIfNeeded()
    {
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
            infoText = infoGO.AddComponent<TMPro.TextMeshProUGUI>();
            infoText.gameObject.SetActive(false);
        }

        if (noFishMessage == null)
        {
            noFishMessage = new GameObject("NoFishMessage");
            noFishMessage.transform.SetParent(transform);
            noFishMessage.AddComponent<TMPro.TextMeshProUGUI>().text = "No fish yet!";
            noFishMessage.SetActive(false);
        }

        Debug.Log("[Aquarium] InitializeIfNeeded completado.");
    }

    public void RefreshAquarium()
    {
        // Destruye instancia anterior
        if (currentFishInstance != null)
        {
            Destroy(currentFishInstance);
            currentFishInstance = null;
        }

        // Limpia contenedor
        foreach (Transform child in aquariumContainer)
            Destroy(child.gameObject);

        // Mostrar mensaje si no hay peces
        if (unlockedFishPrefabs.Count == 0)
        {
            noFishMessage.SetActive(true);
            return;
        }
        else
        {
            noFishMessage.SetActive(false);
        }

        // Instancia el pez actual
        currentIndex = Mathf.Clamp(currentIndex, 0, unlockedFishPrefabs.Count - 1);
        SpawnFish(unlockedFishPrefabs[currentIndex]);
    }

    public void SpawnFish(GameObject prefab)
    {
        currentFishInstance = Instantiate(prefab, aquariumContainer);

        // Asegurar Animator activo y reseteado
        Animator anim = currentFishInstance.GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = true;
            anim.Play(0, -1, 0f);
        }
    }

    #endregion

    #region Navigation / Info

    public void NextFish()
    {
        if (unlockedFishPrefabs.Count == 0) return;
        currentIndex = (currentIndex + 1) % unlockedFishPrefabs.Count;
        RefreshAquarium();
    }

    public void PreviousFish()
    {
        if (unlockedFishPrefabs.Count == 0) return;
        currentIndex = (currentIndex - 1 + unlockedFishPrefabs.Count) % unlockedFishPrefabs.Count;
        RefreshAquarium();
    }

    public void ToggleFishView()
    {
        if (currentFishInstance == null) return;

        isShowingInfo = !isShowingInfo;

        if (isShowingInfo)
        {
            ShowFishInfo();
        }
        else
        {
            if (infoText != null) infoText.gameObject.SetActive(false);
            currentFishInstance.SetActive(true);
        }
    }

    public void ShowFishInfo()
    {
        if (currentFishInstance != null)
            currentFishInstance.SetActive(false);

        if (infoText != null)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = unlockedFishPrefabs[currentIndex].name; // puedes poner info detallada
        }
    }

    #endregion
}
