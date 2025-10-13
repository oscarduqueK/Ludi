using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("�Has perdido!");
            Destroy(gameObject);
            GameManager.Instance.GameOver(false);
        }
    }
}
