using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    string siguienteEscena;

    // Use this for initialization
    void Start () {
        SceneManager.LoadSceneAsync(siguienteEscena, LoadSceneMode.Single);
    }
}
