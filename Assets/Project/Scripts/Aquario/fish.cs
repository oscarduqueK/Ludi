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

    [HideInInspector]
    public bool isInitialized = false;

    /// <summary>
    /// Inicializa el pez con sus datos.
    /// </summary>
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

        isInitialized = true;
    }

    /// <summary>
    /// Método que puede sobrescribirse para animaciones o efectos al aparecer.
    /// </summary>
    public virtual void OnSpawn()
    {
        if (!isInitialized)
            Debug.LogWarning($"El pez {fishName} no ha sido inicializado antes de aparecer.");
    }

    /// <summary>
    /// Método que puede sobrescribirse para efectos al desbloquear.
    /// </summary>
    public virtual void OnUnlock()
    {
        if (animator != null)
            animator.SetTrigger("Unlock");
    }
}
