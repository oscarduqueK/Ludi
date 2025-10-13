using UnityEngine;
using UnityEngine.SceneManagement;

public class callScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (testSingleton.Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject); // opcional, si quieres que sobreviva a los cambios de escena
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    void Update()
    {
        //Load scene 1
        Scene scene = SceneManager.GetSceneByName("MenuPausa");
        if (Input.GetKeyDown(KeyCode.Escape) && !scene.isLoaded)
        {
            SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync("MenuPausa");
        }
    }
}