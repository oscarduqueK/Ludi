using UnityEngine;

public abstract class Fish
{
    protected GameObject prefab;        // Prefab del pez
    protected GameObject instance;      // Instancia actual
    protected Transform container;      // Dónde se muestra
    protected GameObject infoPanel;     // Panel de info
    protected bool fishSpriteActive;    // Estado actual

    public void Initialize(Transform container, GameObject infoPanel)
    {
        this.container = container;
        this.infoPanel = infoPanel;
    }

    public void SetActiveMode(bool showFish)
    {
        fishSpriteActive = showFish;

        if (showFish)
            ShowFish();
        else
            FishInfo();
    }

    public abstract GameObject GetPrefab(); // Devuelve el prefab de cada pez
    public abstract void SetupInfo();       // Define su info textual

    public virtual void ShowFish()
    {
        Clear();
        prefab = GetPrefab();

        if (prefab != null)
            instance = GameObject.Instantiate(prefab, container);

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    public virtual void FishInfo()
    {
        Clear();
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
            SetupInfo();
        }
    }

    public virtual void Clear()
    {
        if (instance != null)
            GameObject.Destroy(instance);
    }
}
