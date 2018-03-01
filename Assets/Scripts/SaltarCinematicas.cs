using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltarCinematicas : MonoBehaviour
{
    [SerializeField]
    string siguienteEscena;

    private Analytics gAna;
    private LevelManager lm;
    private MeGustoNoMeGusto mg;
    public MovieTexture movTexture;
    public MovieTexture movTexture2;

    // Use this for initialization
    void Start()
    {
        gAna = FindObjectOfType<Analytics>();
        lm = FindObjectOfType<LevelManager>();
        mg = FindObjectOfType<MeGustoNoMeGusto>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible=true;
        StartCoroutine(ShowVideos());
    }

    private IEnumerator ShowVideos()
    {
        bool skipped = false;

        GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();

        while (movTexture.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                skipped = true;

                if (lm.FirstLoad)
                {
                    gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("NoCinematicasHastaFinal")
                    .SetEventAction(SceneManager.GetActiveScene().name));
                    gAna.gv4.DispatchHits();
                }

                if (siguienteEscena != null && siguienteEscena != "")
                {
                    SceneManager.LoadScene(siguienteEscena);
                    yield break;
                }

                movTexture.Stop();
            }

            yield return null;
        }

        if (!skipped && lm.FirstLoad)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("CinematicasHastaFinal")
            .SetEventAction(SceneManager.GetActiveScene().name));
            gAna.gv4.DispatchHits();
        }

        foreach (var audioSource in gameObject.GetComponents<AudioSource>())
        {
            audioSource.Stop();
        }

        var audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = movTexture2.audioClip;
        audioSource2.Play();
        GetComponent<Renderer>().material.mainTexture = movTexture2;
        movTexture2.Play();

        while (movTexture2.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (siguienteEscena != null && siguienteEscena != "")
                {
                    SceneManager.LoadScene(siguienteEscena);
                    yield break;
                }

                movTexture2.Stop();
            }

            yield return null;
        }

        mg.Mostrar();
        DestroyObject(gameObject);
    }
}
