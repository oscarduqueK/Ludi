using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(Wait(1.0f));
        // Singleton: evitar duplicados del GameInitializer
        GameInitializer existing = FindAnyObjectByType<GameInitializer>();
        if (existing != null && existing != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // persistente

        // Crear fishUnlockement si no existe
        if (fishUnlockement.Instance == null)
        {
            GameObject fuGO = new GameObject("fishUnlockement");
            fuGO.AddComponent<fishUnlockement>();
            DontDestroyOnLoad(fuGO);
        }
    }

    private void Start()
    {
        StartCoroutine(Wait(2.0f));
        // Cargar la siguiente escena
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
