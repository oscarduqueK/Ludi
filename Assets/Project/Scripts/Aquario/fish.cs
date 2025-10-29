using UnityEngine;
using TMPro;

public class Fish : MonoBehaviour
{
    [Header("Datos")]
    public int fishId;
    public string fishName;

    [Header("Componentes opcionales")]
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI InterrogationText;

    public bool isInitialized = false;

    [SerializeField] public bool isFishUnlocked = false;
    public bool IsFishUnlocked => isFishUnlocked;


    public virtual void Initialize(FishData data)
    {
        if (data == null)
        {
            Debug.LogWarning("Intento de inicializar un pez sin datos.");
            return;
        }

        fishId = data.id;
        fishName = data.fishName;

        if (nameText != null)
            nameText.text = fishName;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        isFishUnlocked = data.unlocked;

        isInitialized = true;
    }

    public virtual string GetInfoString()
    {
        if (!isFishUnlocked) return "???";
        return fishName ?? "Unknown Fish";
    }

    public virtual void OnSpawn()
    {
        if (!isInitialized)
            Debug.LogWarning($"El pez {fishName} no ha sido inicializado antes de aparecer.");
    }

    public virtual void OnUnlock()
    {
        isFishUnlocked = true;

        if (animator != null)
            animator.SetTrigger("Unlock");
    }
}
