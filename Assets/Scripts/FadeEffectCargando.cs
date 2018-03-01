using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeEffectCargando : MonoBehaviour
{

    public float minimum = 0.0f;
    public float maximum = 1f;

    [SerializeField]
    public float duration = 0.5f;

    private float tiempo;
    public Text cargando;

    bool up = true;

    void Start()
    {
        tiempo=0;
    }
    void Update()
    {
        if (up)
        {
            tiempo += Time.deltaTime;
            if (tiempo>duration)
            {
                tiempo = duration;
                up = false;
            }
        }
        else
        {
            tiempo -= Time.deltaTime;
            if(tiempo<0)
            {
                tiempo = 0;
                up = true;
            }
        }

        float t = tiempo / duration;

        cargando.color= new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
    }
}
