using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class CanvasComprobandoTeoria : CanvasReproductor
{
    private Camera camera;
    public VideoClip clip;

    private void Start()
    {
        LapsoDeTiempo = 8f;
        camera = FindObjectOfType<Camera>();
        movie = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
        movie.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        movie.clip = clip;
        movie.Play();
    }

    private void Update()
    {
        LapsoDeTiempo -= Time.deltaTime;
        if (LapsoDeTiempo <= 0)
        {
            Ocultar();
        }
    }
}
