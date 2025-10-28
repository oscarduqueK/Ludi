using UnityEngine;
using UnityEngine.Audio;

public class Basura : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip hitByBallSound;
    public AudioClip hitByOtherSound;
    public AudioMixerGroup outputMixerGroup;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = outputMixerGroup;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (hitByBallSound != null)
                audioSource.PlayOneShot(hitByBallSound);

            GameManager.Instance.AddTrash(1);
            Destroy(gameObject, 0.05f);
        }
        else if (collision.gameObject.CompareTag("Rampa") ||
                 collision.gameObject.CompareTag("Flipper") ||
                 collision.gameObject.CompareTag("Ground"))
        {
            if (hitByOtherSound != null)
                audioSource.PlayOneShot(hitByOtherSound);

            GameManager.Instance.RestarVida();
            Destroy(gameObject, 0.05f);
        }
    }
}
