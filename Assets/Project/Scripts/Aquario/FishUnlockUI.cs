using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FishUnlockUI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform spawnPoint;
    public TextMeshProUGUI fishNameText;
    public TextMeshProUGUI titleText;

    private Fish currentFishInstance;

    void Start()
    {
        ShowUnlockedFish();
    }

    void ShowUnlockedFish()
    {
        int fishId = PlayerPrefs.GetInt("LastUnlockedFish", -1);

        if (fishId == -1)
        {
            Debug.LogWarning("No hay pez desbloqueado registrado.");
            titleText.text = "¡No hay pez nuevo!";
            return;
        }

        FishData data = FishDatabase.Instance.GetFishData(fishId);

        if (data == null)
        {
            Debug.LogError($"No se encontró FishData con ID {fishId}");
            titleText.text = "Error al cargar pez";
            return;
        }

        GameObject fishPrefab = data.prefab;
        if (fishPrefab == null)
        {
            Debug.LogError("El prefab del pez está vacío en FishData.");
            return;
        }

        GameObject instance = Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
        currentFishInstance = instance.GetComponent<Fish>();

        currentFishInstance.Initialize(data);
        currentFishInstance.isInitialized = true;

        if (data.unlocked)
        {
            currentFishInstance.OnUnlock();
        }

        currentFishInstance.OnSpawn();

        titleText.text = "¡Nuevo pez desbloqueado!";
        fishNameText.text = data.fishName;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
