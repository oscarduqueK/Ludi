using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SetLevel : MonoBehaviour
{
    public Button[] levelbuttons;
    public string gameplaySceneName = "Pinball";

    private void Start()
    {
        for (int i = 0; i < levelbuttons.Length; i++)
        {
            int levelIndex = i + 1;
            levelbuttons[i].onClick.AddListener(() => LoadLevel(levelIndex)); 
        }
    }

    void LoadLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelToLoad", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(gameplaySceneName);
    }
}
