using UnityEngine;

public class Basura : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con la pelota
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // elimina la basura
        }
    }
}
