using UnityEngine;

public class Aquarium : MonoBehaviour
{
    public Transform fishContainer;
    public GameObject fishInfoPanel;
    public GameObject noFishesYetMessage;

    public bool fishSpriteActive = true;

    private int currentFishIndex = 0;
    private Fish currentFish;

    void Start()
    {
        UpdateAquariumView();
    }

    public void UpdateAquariumView()
    {
        if (currentFish != null)
            currentFish.Clear();

        if (noFishesYetMessage != null)
            noFishesYetMessage.SetActive(false);

        if (GetTotalUnlockedFishes() == 0)
        {
            if (noFishesYetMessage != null)
                noFishesYetMessage.SetActive(true);
            return;
        }

        // Selecciona la clase correspondiente
        switch (currentFishIndex)
        {
            //case 0: currentFish = new FishSample1(); break;
            //case 1: currentFish = new FishSample2(); break;
            case 2: currentFish = new FishSample3(); break;
            default: currentFish = null; break;
        }

        if (currentFish == null) return;

        currentFish.Initialize(fishContainer, fishInfoPanel);
        currentFish.SetActiveMode(fishSpriteActive);
    }

    private int GetTotalUnlockedFishes()
    {
        return 3; // temporal, simula que hay 3 peces desbloqueados
    }

    public void ToggleView()
    {
        fishSpriteActive = !fishSpriteActive;
        UpdateAquariumView();
    }

    public void NextFish()
    {
        currentFishIndex = (currentFishIndex + 1) % GetTotalUnlockedFishes();
        UpdateAquariumView();
    }

    public void PreviousFish()
    {
        currentFishIndex = (currentFishIndex - 1 + GetTotalUnlockedFishes()) % GetTotalUnlockedFishes();
        UpdateAquariumView();
    }
}
