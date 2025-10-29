using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlVolumenEfectos : MonoBehaviour
{
    public AudioMixer mixerEfectos;
    public Slider slider;
    public string parametroVolumen = "SonidoVol";

    [Header("Rango de dB")]
    public float minDB = -60f;
    public float maxDB = 10f;
    private const string PREF_KEY = "VolumenEfectos";

    void Start()
    {
        float valorGuardado = PlayerPrefs.GetFloat(PREF_KEY, 1f);
        float dB = Mathf.Lerp(minDB, maxDB, valorGuardado);
        mixerEfectos.SetFloat(parametroVolumen, dB);

        if (slider != null)
        {
            slider.value = valorGuardado;
            slider.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    public void CambiarVolumen(float valor)
    {
        float dB = Mathf.Lerp(minDB, maxDB, valor);
        mixerEfectos.SetFloat(parametroVolumen, dB);

        PlayerPrefs.SetFloat(PREF_KEY, valor);
    }
}
