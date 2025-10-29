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
        // Suscribirse al evento
        Aquarium.OnFishChanged += ClearInfo;
    }

    private void OnDisable()
    {
        // Evitar errores al salir de escena
        Aquarium.OnFishChanged -= ClearInfo;
    }

    // Se ejecuta automáticamente al cambiar de pez
    private void ClearInfo()
    {
        if (infoText != null)
        {
            infoText.text = "";
            infoText.gameObject.SetActive(false);
        }
    }

    public void Learn()
    {
        FishData data = aquarium != null ? aquarium.GetCurrentFishData() : null;
        if (data == null || !data.unlocked)
        {
            infoText.text = "Encara no has desbloquejat aquest peix";
            return;
        }

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance == null) return;

        currentFish = currentFishInstance.GetComponent<Fish>();
        currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();

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
        if (data == null || !data.unlocked)
        {
            infoText.text = "";
            return;
        }

        currentFishInstance = aquarium.GetCurrentFishInstance();
        if (currentFishInstance == null) return;

        currentFish = currentFishInstance.GetComponent<Fish>();
        currentRenderer = currentFishInstance.GetComponent<SpriteRenderer>();

        if (currentRenderer != null)
            currentRenderer.enabled = true;

        infoText.gameObject.SetActive(false);

        if (currentFish != null)
            currentFish.OnSpawn();
    }
}
