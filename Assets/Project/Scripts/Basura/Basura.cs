using JetBrains.Annotations;
using UnityEngine;

public class Basura : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.AddTrash(1);
        }
        else if (collision.gameObject.CompareTag("Rampa") || collision.gameObject.CompareTag("Flipper") || collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.RestarVida();
            Destroy(this.gameObject);
        }
    }
}


