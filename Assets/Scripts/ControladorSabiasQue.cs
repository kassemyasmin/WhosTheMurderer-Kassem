using UnityEngine;
using UnityEngine.UI;

public class ControladorSabiasQue : MonoBehaviour {

    [SerializeField]
    float TiempoEspera;

    private float tiempoRestante;
    private Text tip;

    private LineaSabiasQue[] lineas;

    // Use this for initialization
    void Start () {
        tiempoRestante = TiempoEspera;

        lineas = GetComponentsInChildren<LineaSabiasQue>();
        foreach(var t in GetComponentsInChildren<Text>())
        {
            if (t.name =="Tip")
                tip = t;
        }
        Next();
	}

    // Update is called once per frame
    void Update()
    {

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            Next();
            tiempoRestante = TiempoEspera;
        }
    }

    private void Next()
    {
        System.Random rnd = new System.Random();

        tip.text = lineas[rnd.Next(0, lineas.GetLength(0) - 1)].Tip;
    }
}
