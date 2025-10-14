using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    public void GameOver(bool won)
    {
        if (gameEnded)
        {
            return;
        }
        gameEnded = true;

        if (won)
        {
            Debug.Log("¡Has ganado!");
            Time.timeScale = 0f;
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("¡Has perdido!");
            Time.timeScale = 0f;
            SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
        }
    }
    void Update()
    {
        if (gameEnded)
        {
            return;
        }
        if (GameObject.FindGameObjectsWithTag("Basura").Length == 0)
        {
            GameOver(true);
        }
    }
}
