using UnityEngine;
using System.Collections;
using System;

public class Cronometro : MonoBehaviour {


    public float TiempoTranscurrido { get; private set; }
    public string ReadableTime {
        get
        {
            return GetTexto();
        }
    }

    private bool active = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (active)
            TiempoTranscurrido += Time.deltaTime;
    }

    public void ResetMeasurement()
    {
        TiempoTranscurrido = 0;
    }

    public void StartMeasurement()
    {
        active = true;
    }

    public void StopMeasurement()
    {
        active = false;
    }

    private string  GetTexto()
    {
        int minutes = Mathf.RoundToInt(Mathf.Floor(TiempoTranscurrido / 60));
        int seconds = Mathf.RoundToInt(TiempoTranscurrido % 60);

        var minutesText = Convert.ToString(minutes);
        if (minutesText.Length == 1) minutesText = "0" + minutesText;
        var secondsText = Convert.ToString(seconds);
        if (secondsText.Length == 1) secondsText = "0" + secondsText;
        return minutesText + ":" + secondsText;
    }
}
