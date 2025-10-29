using UnityEngine;
using UnityEngine.Audio;

public class Ball : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip bounceSound;
    public AudioMixerGroup outputMixerGroup;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = outputMixerGroup;
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground") || !collision.gameObject.CompareTag("Basura"))
        {
            if (bounceSound != null)
                audioSource.PlayOneShot(bounceSound);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver(false);
        }
    }
}
