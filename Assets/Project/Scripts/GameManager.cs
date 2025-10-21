using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public int requiredTrashToWin;

    private int trashCollected = 0;

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
        Debug.Log("suma");

        if (trashCollected >= requiredTrashToWin)
        {
            GameOver(true);
        }
    }

    public void GameOver(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (won)
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }
        else
        {
            SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }
    }
    public void ResetGame()
    {
        trashCollected = 0;
        gameEnded = false;
        Time.timeScale = 1f;
    }
}
