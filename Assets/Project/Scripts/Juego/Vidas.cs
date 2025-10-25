using UnityEngine;
using System.Collections;

public class Vidas : MonoBehaviour
{
    [SerializeField] private GameObject[] corazones;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
        ActualizarVidas();
    }

    public void ActualizarVidas()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < gm.vidas)
            {
                corazones[i].SetActive(true);
                var sr = corazones[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = 1f;
                    sr.color = c;
                }
            }
            else
            {
                var sr = corazones[i].GetComponent<SpriteRenderer>();
                if (sr != null && corazones[i].activeSelf)
                {
                    StartCoroutine(FadeOut(sr));
                }
            }
        }
    }

    private IEnumerator FadeOut(SpriteRenderer sr)
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 2f;
            Color c = sr.color;
            c.a = t;
            sr.color = c;
            yield return null;
        }
        sr.gameObject.SetActive(false);
    }
}
