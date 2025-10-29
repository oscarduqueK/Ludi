using UnityEngine;
using TMPro;

public class FishInfoPanel : MonoBehaviour
{
    public Aquarium aquarium;
    public TextMeshProUGUI infoText;

    private GameObject currentFishInstance;
    private SpriteRenderer currentRenderer;
    private Fish currentFish;

    void Start()
    {
        if (infoText == null)
            infoText = FindAnyObjectByType<TextMeshProUGUI>();
    }

    public void Learn()
    {
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;
        if (data == null || !data.unlocked)
        {
            infoText.text = "";
            return;
        }

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance == null) return;

        currentFish = currentFishInstance.GetComponent<Fish>();
        currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();

        // Ocultar el pez visualmente
        if (currentRenderer != null)
            currentRenderer.enabled = false;

        // Mostrar texto
        infoText.gameObject.SetActive(true);
        infoText.text = currentFish != null ? currentFish.GetInfoString() : $"<b>{data.fishName}</b>\n\n(Info no disponible)";
    }

    public void View()
    {
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;
        if (data == null || !data.unlocked)
        {
            infoText.text = "";
            return;
        }

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance == null) return;

        currentFish = currentFishInstance.GetComponent<Fish>();
        currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();

        // Mostrar pez
        if (currentRenderer != null)
            currentRenderer.enabled = true;

        // Ocultar texto
        infoText.gameObject.SetActive(false);

        // Reanudar comportamiento
        if (currentFish != null)
            currentFish.OnSpawn();
    }
}
