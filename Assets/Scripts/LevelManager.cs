using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 cursorSpot = Vector2.zero;
    private int loadCount=0;

    Analytics gAna;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, cursorSpot, cursorMode);
        gAna = FindObjectOfType<Analytics>();
        FirstLoad = true;
    }

    public bool FirstLoad { get; private set;}

    public void LoadScene(string nameYReiniciar)
    {
        char[]sep ={';'};
        string[] pars = nameYReiniciar.Split(sep);

        string name = pars[0];
        bool reiniciar;

        if (name.StartsWith("Caso"))
            loadCount++;

        if (loadCount > 1)
            FirstLoad = false;

        if (pars.GetLength(0) > 1)
            reiniciar = pars[1] == "S";
        else
            reiniciar = false;
            
        SceneManager.LoadScene(name);
       // SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        if (name == "Creditos")
        {
            gAna.gv4.LogScreen(new AppViewHitBuilder()
            .SetScreenName("Creditos"));
            gAna.gv4.DispatchHits();
        }

        if (reiniciar)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("ReiniciarCaso")
            .SetEventAction(SceneManager.GetActiveScene().name));
            gAna.gv4.DispatchHits();
        }

        if (name == "Menu")
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("VolverAlMenu")
            .SetEventAction(SceneManager.GetActiveScene().name));
            gAna.gv4.DispatchHits();
        }
    }

    public void QuitGame()
    {

        gAna.gv4.LogEvent(new EventHitBuilder()
        .SetEventCategory("Salir")
        .SetEventAction(SceneManager.GetActiveScene().name));
        gAna.gv4.DispatchHits();

        Application.Quit();
    }

}
