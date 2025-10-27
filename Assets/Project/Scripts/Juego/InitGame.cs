using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameInitializer : MonoBehaviour
{
    public string sceneToLoad = "MainMenu"; // la escena que viene después de la inicialización

    void Awake()
    {
        // Inicializar FishDatabase si no existe
        if (FishDatabase.Instance == null)
        {
            GameObject dbGO = new GameObject("FishDatabase");
            dbGO.AddComponent<FishDatabase>();
        }

        // Inicializar FishUnlockement si no existe
        if (fishUnlockement.Instance == null)
        {
            GameObject fuGO = new GameObject("fishUnlockement");
            fuGO.AddComponent<fishUnlockement>();
        }

        // Si quieres, puedes inicializar otros sistemas globales aquí

        DontDestroyOnLoad(gameObject); // El loader también persiste si lo deseas
    }

    IEnumerator Start()
    {
        // Pequeña espera para simular carga
        yield return new WaitForSeconds(3f);

        // Cargar la siguiente escena
        SceneManager.LoadScene("Menu");
    }
}
