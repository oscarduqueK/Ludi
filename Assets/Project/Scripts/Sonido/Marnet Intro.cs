using UnityEngine;
using System.Collections;

public class MarnetIntro : MonoBehaviour
{
    public float startFadeTime = 2f;
    public float endFadeTime = 3.5f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
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

        audioSource.volume = 0f;
    }
}
