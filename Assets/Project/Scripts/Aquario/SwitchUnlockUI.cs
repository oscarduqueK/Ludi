using UnityEngine;
using TMPro;

public class FishInfoPanel : MonoBehaviour
{
    public Aquarium aquarium;
    public TextMeshProUGUI infoText;

    private GameObject currentFishInstance;
    private SpriteRenderer currentRenderer;
    private Fish currentFish;

    private void OnEnable()
    {
        Aquarium.OnFishChanged += ClearInfo;
    }

    private void OnDisable()
    {
        Aquarium.OnFishChanged -= ClearInfo;
    }

    private void ClearInfo()
    {
        ResetVisualState();
    }

    public void Learn()
    {
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;
        if (data == null)
            return;

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance != null)
        {
            currentFish = currentFishInstance.GetComponent<Fish>();
            currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();
        }

        if (!data.unlocked)
        {
            if (currentRenderer != null)
                currentRenderer.enabled = false;

            infoText.gameObject.SetActive(true);
            infoText.text = "Encara no has desbloquejat aquest peix\n\n???";
            return;
        }

        if (currentRenderer != null)
            currentRenderer.enabled = false;

        infoText.gameObject.SetActive(true);
        infoText.text = currentFish != null
            ? currentFish.GetInfoString()
            : $"<b>{data.fishName}</b>\n\n(Info no disponible)";
    }

    public void View()
    {
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;
        if (data == null)
            return;

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance != null)
        {
            currentFish = currentFishInstance.GetComponent<Fish>();
            currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();
        }

        if (!data.unlocked)
        {
            if (currentRenderer != null)
                currentRenderer.enabled = false;

            infoText.gameObject.SetActive(true);
            infoText.text = "???";
            return;
        }

        if (currentRenderer != null)
            currentRenderer.enabled = true;

        infoText.gameObject.SetActive(false);

        if (currentFish != null)
            currentFish.OnSpawn();
    }

    private void ResetVisualState()
    {

        if (infoText != null)
        {
            infoText.text = "";
            infoText.gameObject.SetActive(false);
        }

        currentFishInstance = aquarium != null ? aquarium.GetCurrentFishInstance() : null;
        if (currentFishInstance != null)
        {
            currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();
            if (currentRenderer != null)
                currentRenderer.enabled = true;
        }
    }
}
