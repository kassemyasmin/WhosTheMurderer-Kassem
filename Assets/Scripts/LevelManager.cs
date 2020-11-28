using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 cursorSpot = Vector2.zero;
    private int loadCount=0;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, cursorSpot, cursorMode);
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
            UnityEngine.Analytics.Analytics.CustomEvent("Creditos");

            UnityEngine.Analytics.Analytics.FlushEvents();
        }

        if (reiniciar)
        {

            UnityEngine.Analytics.Analytics.CustomEvent("ReiniciarCaso", new Dictionary<string, object>
                        {
                            {
                                "Scene", SceneManager.GetActiveScene().name
                            }
                        });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }

        if (name == "Menu")
        {
            UnityEngine.Analytics.Analytics.CustomEvent("VolverAlMenu", new Dictionary<string, object>
                        {
                            {
                                "Scene", SceneManager.GetActiveScene().name
                            }
                        });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
    }

    public void QuitGame()
    {

        UnityEngine.Analytics.Analytics.CustomEvent("Salir", new Dictionary<string, object>
                        {
                            {
                                "Scene", SceneManager.GetActiveScene().name
                            }
                        });

        UnityEngine.Analytics.Analytics.FlushEvents();

        Application.Quit();
    }

}
