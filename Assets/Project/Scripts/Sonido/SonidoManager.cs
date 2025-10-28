using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlVolumenEfectos : MonoBehaviour
{
    public AudioMixer mixerEfectos;        // Tu AudioMixer de efectos
    public Slider slider;                  // Slider de la UI
    public string parametroVolumen = "SonidoVol"; // Nombre del parámetro expuesto en el mixer

    [Header("Rango de dB")]
    public float minDB = -60f;
    public float maxDB = 10f;
    private const string PREF_KEY = "VolumenEfectos"; // Para guardar el valor

    void Start()
    {
        // Cargar valor guardado (0–1) o usar 1 por defecto
        float valorGuardado = PlayerPrefs.GetFloat(PREF_KEY, 1f);

        // Aplicar al mixer en dB
        float dB = Mathf.Lerp(minDB, maxDB, valorGuardado);
        mixerEfectos.SetFloat(parametroVolumen, dB);

        // Configurar slider
        if (slider != null)
        {
            slider.value = valorGuardado;
            slider.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    public void CambiarVolumen(float valor)
    {
        // Convertir 0–1 del slider a dB
        float dB = Mathf.Lerp(minDB, maxDB, valor);
        mixerEfectos.SetFloat(parametroVolumen, dB);

        // Guardar valor para próximas partidas
        PlayerPrefs.SetFloat(PREF_KEY, valor);
    }
}
