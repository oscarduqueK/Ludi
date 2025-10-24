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
    public int vidas = 3;

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

        if (vidas <= 0)
        {
            GameOver(false);
        }
    }

    public void GameOver(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (won)
        {
            int fishId = tm.currentLevel.GetFishId();

            bool unlockedNow = false;
            if (fu != null && fishId >= 0)
            {
                unlockedNow = fu.UnlockSequence(fishId);
            }

            if (unlockedNow)
                SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            else
                SceneManager.LoadScene("Win4secondTime", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
        }

        Time.timeScale = 0f;
    }

    private void TryToUnlockFish()
    {
        if (tm.currentLevel == null) return;

        int fishId = tm.currentLevel.GetFishId();


        if (fu != null && fishId >= 0)
        {
            currentFish = fu.GetFish(fishId);
            fu.UnlockSequence(fishId);
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
