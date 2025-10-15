using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int requiredTrashToWin = 2;
    private int trashCollected = 0;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
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
            Time.timeScale = 0f; 
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);
        }
        else
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
        }
    }
}
