using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider slider;
    public string parametroVolumen = "MusicVol";

    [Header("Rango de dB")]
    public float minDB = -60f;
    public float maxDB = 10f;
    private const string PREF_KEY = "VolumenMusica";

    void Start()
    {
        float valorGuardado = PlayerPrefs.GetFloat(PREF_KEY, 1f);

        float dB = Mathf.Lerp(minDB, maxDB, valorGuardado);
        musicMixer.SetFloat(parametroVolumen, dB);

        if (slider != null)
        {
            slider.value = valorGuardado;
            slider.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    public void CambiarVolumen(float valor)
    {
        float dB = Mathf.Lerp(minDB, maxDB, valor);
        musicMixer.SetFloat(parametroVolumen, dB);

        PlayerPrefs.SetFloat(PREF_KEY, valor);
    }
}
