using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public int requiredTrashToWin;

    private int trashCollected = 0;

    private bool gameEnded = false;

    public bool isImmortal;

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
        if (isImmortal && !won)
        {
            Debug.Log("El jugador es inmortal, ignorando derrota");
            return;
        }

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
