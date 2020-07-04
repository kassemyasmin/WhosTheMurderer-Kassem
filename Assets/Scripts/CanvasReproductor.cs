using UnityEngine;

public class CanvasReproductor : ControladorCanvasTemporizado {

    
    protected UnityEngine.Video.VideoPlayer movie;

    [SerializeField]
    public bool loop = true;

    public override void Mostrar()
    {
        base.Mostrar();
        movie.Play();
    }

    public override void Ocultar()
    {
        base.Ocultar();
        movie.Stop();
    }

    void OnGUI()
    {
        movie.isLooping = loop;
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie.);
    }

}
