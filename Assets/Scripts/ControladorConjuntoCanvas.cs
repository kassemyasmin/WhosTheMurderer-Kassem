using UnityEngine;
using System.Collections.Generic;

public class ControladorConjuntoCanvas : MonoBehaviour
{

    private Stack<ControladorCanvas> canvasActivos = new Stack<ControladorCanvas>();
    ControladorCursor cursor;

    // Use this for initialization
    void Start()
    {
        cursor = FindObjectOfType<ControladorCursor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushCanvas(ControladorCanvas _canvas)
    {
     /*   if (canvasActivos.Count == 0)
            cursor.Togle();*/
        canvasActivos.Push(_canvas);
    }

    public ControladorCanvas PopCanvas()
    {
        var resp= canvasActivos.Pop();
    /*    if (canvasActivos.Count == 0)
            cursor.Togle();*/
        return resp;
    }

    public bool Escape()
    {
        if (canvasActivos.Count == 0)
            return false;
        else
        {
            var canvas = canvasActivos.Peek();
            canvas.Ocultar();
            return true;
        }
    }

}
