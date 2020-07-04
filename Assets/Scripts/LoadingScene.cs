using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    string siguienteEscena;
	
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(LoadScene(siguienteEscena));
    }

    private IEnumerator LoadScene(string _scene)
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        var async = SceneManager.LoadSceneAsync(siguienteEscena, LoadSceneMode.Single);
        async.allowSceneActivation = true;
        while (!async.isDone)
        {
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene _scene,LoadSceneMode _mode)
    {
        SceneManager.SetActiveScene(_scene);
    }
}
