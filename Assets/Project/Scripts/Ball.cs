using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("¡Has perdido!");
            Destroy(gameObject); // elimina la pelota
            // Opcional: avisar al GameManager
            GameManager.Instance.GameOver(false);
        }
    }
}
