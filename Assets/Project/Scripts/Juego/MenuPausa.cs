using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaUI : MonoBehaviour
{
    public static string previousScene = "";

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("MenuPausa");
        Time.timeScale = 1f;
    }

    public void GoToOptions()
    {
        previousScene = SceneManager.GetActiveScene().name;

        if (previousScene == "Pinball")
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("MenuOptions", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("MenuOptions", LoadSceneMode.Additive);
        }
    }
    public void GoToPausa()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("MenuPausa", LoadSceneMode.Additive);
    }

    public void GoBackFromOptions()
    {
            SceneManager.UnloadSceneAsync("MenuOptions");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GoToSelectorLevel1()
    {
        SceneManager.LoadScene("LevelSelector1");
        Time.timeScale = 1f;
    }
    public void GoToSelectorLevel2()
    {
        SceneManager.LoadScene("LevelSelector2");
        Time.timeScale = 1f;
    }
    public void GoToAquario()
    {
        SceneManager.LoadScene("Aquario");
    }

    public void GoToSaveFish()
    {
        SceneManager.LoadScene("Fish");
    }
    public void ExitFromTheGame()
    {
        Application.Quit();
    }
}
