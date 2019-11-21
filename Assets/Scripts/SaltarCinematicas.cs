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

    // Use this for initialization
    void Start()
    {
        gAna = FindObjectOfType<Analytics>();
        lm = FindObjectOfType<LevelManager>();
        mg = FindObjectOfType<MeGustoNoMeGusto>();
        GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible=true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (lm.FirstLoad)
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("NoCinematicasHastaFinal")
                    .SetEventAction(SceneManager.GetActiveScene().name));
                gAna.gv4.DispatchHits();
            }
            SaltarCinematica();
        }

    }

    void SaltarCinematica()
    {
        if (siguienteEscena != null && siguienteEscena != "")
        { 
            SceneManager.LoadScene(siguienteEscena);
        }

        else
        {
            mg.Mostrar();
            movTexture.Stop();
            foreach (var audioSource in gameObject.GetComponents<AudioSource>())
            {
                audioSource.Stop();
            }
            DestroyObject(gameObject);
        }
    }
}
