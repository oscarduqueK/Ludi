using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Instancia fishUnlockement si no existe
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

        if (trashCollected >= requiredTrashToWin)
        {
            GameOver(true);
        }
    }

    public void RestarVida()
    {
        if (gameEnded) return;

        vidas--;
        Debug.Log("Vida perdida. Vidas restantes: " + vidas);

        FindAnyObjectByType<Vidas>()?.ActualizarVidas();

        if (vidas <= 0)
        {
            StartCoroutine(GameOverConRetraso());
        }
    }

    private IEnumerator GameOverConRetraso()
    {
        yield return new WaitForSeconds(1f);
        GameOver(false);
    }

    public void GameOver(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;

        if (won)
        {
            Debug.Log("¡Nivel completado!");

            TryToUnlockFish();

            int lastUnlockedFish = PlayerPrefs.GetInt("LastUnlockedFish", -1);
            Debug.Log (PlayerPrefs.GetInt("LastUnlockedFish", 0));

            if (lastUnlockedFish == -1 || !fu.IsUnlocked(lastUnlockedFish))
            {
                Debug.Log("Ganaste pero no hay pez nuevo, cargando escena de victoria normal...");
                SceneManager.LoadScene("Win4secondTime", LoadSceneMode.Additive); 
            }
        }
        else
        {
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
    }
}
