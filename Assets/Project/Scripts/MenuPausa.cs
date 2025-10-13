using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaUI : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("MenuPausa");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("MenuOptions");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
