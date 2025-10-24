using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aquarium : MonoBehaviour
{
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

        if (fishes.Count > 0)
            UpdateAquariumView();
        else
            noFishMessage.SetActive(true);
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

    private Fish AssociateFish(int index)
    {
        switch (index)
        {
            case 0: return new FishSample1();
            case 1: return new FishSample2();
            case 2: return new FishSample3();
            default: return null;
        }
    }
}
