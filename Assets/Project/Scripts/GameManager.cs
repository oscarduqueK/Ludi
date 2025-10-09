using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string basuraTag = "Basura";
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
        if (gameEnded) return;
        gameEnded = true;

        if (won) Debug.Log("¡Has ganado!");
        else Debug.Log("¡Has perdido!");
    }

    void Update()
    {
        if (gameEnded) return;

        if (GameObject.FindGameObjectsWithTag(basuraTag).Length == 0)
        {
            GameOver(true);
        }
    }
}
