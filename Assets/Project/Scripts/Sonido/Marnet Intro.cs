using UnityEngine;
using System.Collections;

public class MarnetIntro : MonoBehaviour
{
    public float startFadeTime = 2f;   // segundo donde empieza a bajar
    public float endFadeTime = 3.5f;   // segundo donde el volumen llega a 0
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        // Esperar hasta el segundo 2
        yield return new WaitForSeconds(startFadeTime);

        float duration = endFadeTime - startFadeTime;
        float startVolume = audioSource.volume;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        // Aseguramos que queda en 0
        audioSource.volume = 0f;
    }
}
