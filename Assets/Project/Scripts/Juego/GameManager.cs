using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private trashmanagement tm;
    [SerializeField] private fishUnlockement fu;
    FishDatabase db = FishDatabase.Instance;

    private Fish currentFish;

    [HideInInspector]
    public int requiredTrashToWin;

    private int trashCollected = 0;
    public int vidas;

    private bool gameEnded = false;

    [Header("UI")]
    public TMP_Text trashText;
    
    [Header("Audio Clips")]
    public AudioClip victoryClip;
    public AudioClip defeatClip;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup victoryMixerGroup;
    public AudioMixerGroup defeatMixerGroup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        if (fu == null)
        {
            fu = fishUnlockement.Instance;
        }
    }

    public void AddTrash(int amount = 1)
    {
        if (gameEnded) return;

        trashCollected += amount;
        Debug.Log("Basura recogida: " + trashCollected);

        UpdateTrashText();

        if (trashCollected >= requiredTrashToWin)
        {
            GameOver(true);
        }
    }

    private void UpdateTrashText()
    {
        if (trashText != null)
            trashText.text = trashCollected + "/" + requiredTrashToWin;
    }

    public void RestarVida()
    {
        if (gameEnded) return;

        vidas--;
        Debug.Log("Vida perdida. Vidas restantes: " + vidas);

        FindAnyObjectByType<Vidas>()?.ActualizarVidas();

        if (vidas <= 0)
        {
            GameOver(false);
        }
    }

    public void GameOver(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;

        if (won)
        {
            PlayVictorySound();

            Debug.Log("¡Nivel completado!");
            TryToUnlockFish();
        }
        else
        {
            PlayDefeatSound();

            Debug.Log("Has perdido el nivel.");
            SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
        }
    }

    private void TryToUnlockFish()
    {
        if (tm == null || tm.currentLevel == null)
        {
            Debug.LogWarning("TryToUnlockFish: tm o currentLevel null");
            return;
        }

        int fishId = tm.currentLevel.GetFishId();
        Debug.Log($"TryToUnlockFish: fishId={fishId}");

        if (fishUnlockement.Instance != null && fishId >= 0)
        {
            bool unlockedNow = fishUnlockement.Instance.UnlockSequence(fishId);

            if (unlockedNow)
            {
                PlayerPrefs.SetInt("LastUnlockedFish", fishId);
                PlayerPrefs.Save();

                Debug.Log($"GameManager: fish {fishId} desbloqueado ahora, cargando Win");
                SceneManager.LoadScene("Win");
            }
            else
            {
                Debug.Log($"GameManager: fish {fishId} ya estaba desbloqueado, continúa flujo normal");
                SceneManager.LoadScene("Win4secondTime", LoadSceneMode.Additive);
            }
        }
        else
        {
            Debug.LogWarning("TryToUnlockFish: fishUnlockement.Instance es null o fishId inválido");
        }
    }

    public void ResetGame()
    {
        trashCollected = 0;
        vidas = 3;
        gameEnded = false;
        Time.timeScale = 1f;
        UpdateTrashText();
    }

    #region Audio

    private void PlayVictorySound()
    {
        if (victoryClip == null) return;

        GameObject tempAudio = new GameObject("VictoryAudio");
        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.clip = victoryClip;
        source.outputAudioMixerGroup = victoryMixerGroup;
        source.Play();
        Destroy(tempAudio, victoryClip.length);
    }

    private void PlayDefeatSound()
    {
        if (defeatClip == null) return;

        GameObject tempAudio = new GameObject("DefeatAudio");
        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.clip = defeatClip;
        source.outputAudioMixerGroup = defeatMixerGroup;
        source.Play();
        Destroy(tempAudio, defeatClip.length);
    }

    #endregion
}
