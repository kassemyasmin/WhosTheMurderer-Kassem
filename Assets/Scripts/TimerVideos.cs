using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerVideos : MonoBehaviour
{
    public float temporizadorTimer;

   [SerializeField]
    string siguienteEscena;

   // float timer;
    bool timerStarted = false;
    private Analytics gAna;
    private LevelManager lm;

    // Use this for initialization
    void Start()
    {
        StartTimer();
        gAna = FindObjectOfType<Analytics>();
        lm = FindObjectOfType<LevelManager>();
    }

    public void StartTimer()
    {
        //timer = temporizadorTimer;
        timerStarted = true;
        this.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        timerStarted = false;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted == true)
        {
            temporizadorTimer -= Time.deltaTime;
            if (temporizadorTimer <= 0)
            {
                if (lm.FirstLoad)
                {
                    if (SceneManager.GetActiveScene().name == "Creditos")
                    {
                        gAna.gv4.LogEvent(new EventHitBuilder()
                        .SetEventCategory("Creditos")
                        .SetEventAction("EscuchoHastaFinal"));
                        gAna.gv4.DispatchHits();
                    }
                }


                if (siguienteEscena != null && siguienteEscena != "")
                    SceneManager.LoadScene(siguienteEscena);

                StopTimer();
            }
        }
    }
}


