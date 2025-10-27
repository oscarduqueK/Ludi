using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private trashmanagement tm;
    [SerializeField] private fishUnlockement fu;

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

        //// Instancia fishUnlockement si no existe
        //if (fu == null)
        //{
        //    GameObject fuGO = new GameObject("fishUnlockement");
        //    fu = fuGO.AddComponent<fishUnlockement>();
        //}
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

        // Pausa el juego
        Time.timeScale = 0f;

        if (won)
        {
            Debug.Log("¡Nivel completado!");

            // Intentamos desbloquear el pez correspondiente
            TryToUnlockFish();

            // Si no se desbloquea ningún pez nuevo, vamos al menú o escena de victoria normal
            int lastUnlockedFish = PlayerPrefs.GetInt("LastUnlockedFish", -1);

            if (lastUnlockedFish == -1 || !fu.IsUnlocked(lastUnlockedFish))
            {
                // No hay pez desbloqueado, solo ganamos
                Debug.Log("Ganaste pero no hay pez nuevo, cargando escena de victoria normal...");
                SceneManager.LoadScene("Win4secondTime"); // Cambia esto por tu escena de victoria
            }

            // Si sí se desbloqueó un pez nuevo, TryToUnlockFish ya habrá cargado la escena "Win"
            // Por eso aquí no hacemos nada más
        }
        else
        {
            Debug.Log("Has perdido el nivel.");
            SceneManager.LoadScene("Lose"); // Cambia esto por tu escena de derrota
        }
    }

    private void TryToUnlockFish()
    {
        Debug.Log("Niga");
        if (tm == null || tm.currentLevel == null) return;

        int fishId = tm.currentLevel.GetFishId();

        if (fu != null && fishId >= 0)
        {
            bool unlockedNow = fu.UnlockSequence(fishId);

            if (unlockedNow)
            {
                PlayerPrefs.SetInt("LastUnlockedFish", fishId);
                PlayerPrefs.Save();

                Debug.Log($"GameManager: Pez {fishId} desbloqueado, cargando escena Win...");
                SceneManager.LoadScene("Win");
            }
            else
            {
                Debug.Log($"GameManager: Pez {fishId} ya estaba desbloqueado, continúa flujo normal.");
            }
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
