using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(Wait(1.0f));

        GameInitializer existing = FindAnyObjectByType<GameInitializer>();
        if (existing != null && existing != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (fishUnlockement.Instance == null)
        {
            GameObject fuGO = new GameObject("fishUnlockement");
            fuGO.AddComponent<fishUnlockement>();
            DontDestroyOnLoad(fuGO);
        }
    }
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}