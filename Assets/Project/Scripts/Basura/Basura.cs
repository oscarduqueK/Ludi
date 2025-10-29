using UnityEngine;
using UnityEngine.Audio;

public class Basura : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip hitByBallSound;
    public AudioClip hitByOtherSound;
    public AudioMixerGroup outputMixerGroup;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (hitByBallSound != null)
            {
                GameObject tempAudio = new GameObject("TempAudio_BallHit");
                AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
                tempSource.clip = hitByBallSound;
                tempSource.outputAudioMixerGroup = outputMixerGroup;
                tempSource.Play();
                Destroy(tempAudio, hitByBallSound.length);
            }

            GameManager.Instance.AddTrash(1);
            Destroy(gameObject, 0.05f);
        }
        else if (collision.gameObject.CompareTag("Rampa") || collision.gameObject.CompareTag("Flipper") || collision.gameObject.CompareTag("Ground"))
        {
            if (hitByOtherSound != null)
            {
                GameObject tempAudio = new GameObject("TempAudio_OtherHit");
                AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
                tempSource.clip = hitByOtherSound;
                tempSource.outputAudioMixerGroup = outputMixerGroup;
                tempSource.Play();
                Destroy(tempAudio, hitByOtherSound.length);
            }

            GameManager.Instance.RestarVida();
            Destroy(gameObject, 0.05f);
        }
    }
}
