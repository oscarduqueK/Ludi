using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceAnimation : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            Debug.Log("Fin alcanzado — cambiando escena.");
            SceneManager.LoadScene("Menu");
        }
    }
}
