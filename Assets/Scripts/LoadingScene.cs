using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    string siguienteEscena;

    private AsyncOperation loading;

    // Use this for initialization
    void Start () {
        loading=SceneManager.LoadSceneAsync(siguienteEscena,LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update ()
    {
        if (loading.isDone)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(siguienteEscena));
    }
}
