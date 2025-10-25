using UnityEngine;
using UnityEngine.SceneManagement;

public class callScene : MonoBehaviour
{
    void Update()
    {
        //Load scene 1
        Scene scene = SceneManager.GetSceneByName("MenuPausa");
        if (Input.GetKeyDown(KeyCode.Escape) && !scene.isLoaded)
        {
            SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync("MenuPausa");
            Time.timeScale = 1f;
        }
    }
}