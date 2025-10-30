using UnityEngine;

public class ResponsiveResolution : MonoBehaviour
{
    private static ResponsiveResolution instance;

    private int lastWidth;
    private int lastHeight;

    void Awake()
    {
        // Patrón singleton básico para que no se duplique
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        UpdateResolution();
    }

    void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            lastWidth = Screen.width;
            lastHeight = Screen.height;
            UpdateResolution();
        }
    }

    void UpdateResolution()
    {
        // Forzar actualización del viewport interno de Unity
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed);

        // Ajuste opcional para cámaras ortográficas (2D)
        Camera cam = Camera.main;
        if (cam != null && cam.orthographic)
        {
            // Puedes ajustar el 5f según tu escala base
            cam.orthographicSize = (Screen.height / 1080f) * 5f;
        }
    }
}
