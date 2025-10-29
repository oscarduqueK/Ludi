using UnityEngine;
using UnityEngine.EventSystems;

public class BotonSonido : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip sonidoHover;
    public AudioClip sonidoClick;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sonidoHover != null)
            audioSource.PlayOneShot(sonidoHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);
    }
}
