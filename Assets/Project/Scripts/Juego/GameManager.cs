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

        // Instancia fishUnlockement si no existe
        if (fu == null)
        {
            GameObject fuGO = new GameObject("fishUnlockement");
            fu = fuGO.AddComponent<fishUnlockement>();
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

        Time.timeScale = 0f; // pausa el juego mientras se cargan las escenas

        if (won)
        {
            if (tm == null || tm.currentLevel == null)
            {
                Debug.LogWarning("GameOver: tm o currentLevel null");
                SceneManager.LoadScene("Win4secondTime");
                return;
            }

            int fishId = tm.currentLevel.GetFishId();

            if (fishId < 0)
            {
                Debug.LogWarning("GameOver: fishId inválido");
                SceneManager.LoadScene("Win4secondTime");
                return;
            }

            // Asegurarse de que exista Aquarium antes de desbloquear
            if (Aquarium.Instance == null)
            {
                // Intentar encontrar un Aquarium existente en la escena
                Aquarium existing = FindAnyObjectByType<Aquarium>();
                if (existing != null)
                    Aquarium.Instance = existing;
                else
                {
                    // Crear dinámicamente un Aquarium vacío
                    GameObject aquariumGO = new GameObject("Aquarium");
                    Aquarium.Instance = aquariumGO.AddComponent<Aquarium>();
                    DontDestroyOnLoad(aquariumGO);
                    Aquarium.Instance.InitializeIfNeeded();
                }
            }

            bool unlockedNow = fu != null && fu.UnlockSequence(fishId);

            // Cargar la escena de Win de forma consistente
            if (unlockedNow)
            {
                SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("Win4secondTime", LoadSceneMode.Additive);
            }
        }
        else
        {
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

        if (fu != null && fishId >= 0)
        {
            bool unlockedNow = fu.UnlockSequence(fishId);

            // guarda o usa unlockedNow en GameOver o aquí mismo
            if (unlockedNow)
                Debug.Log($"GameManager: fish {fishId} desbloqueado ahora.");
            else
                Debug.Log($"GameManager: fish {fishId} ya estaba desbloqueado.");
        }
        else
        {
            Debug.LogWarning("TryToUnlockFish: fu null o fishId invalido");
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
