using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyButton : MonoBehaviour
{
    public string nextScene;

    public float fullTime;
    private float currentTime;

    private void Start()
    {
        currentTime = fullTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (Input.anyKey && currentTime <= 0)
        {
            NextScene(nextScene); 
        } 
    }

    private void NextScene(string pNextScene)
    {
        SceneManager.LoadScene(pNextScene);
    }
}
