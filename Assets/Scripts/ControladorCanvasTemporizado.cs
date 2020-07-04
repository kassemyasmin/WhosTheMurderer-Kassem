using UnityEngine;

public  class ControladorCanvasTemporizado : ControladorCanvas
    {

    
    public float LapsoDeTiempo;

    float tiempoRestante;

    protected override void CanvasStart()
    {
        base.CanvasStart();
    }

    protected override void CanvasUpdate()
    {
        if (this.Activo)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
                Ocultar();
        }
        base.CanvasUpdate();
    }

    public override void Mostrar()
    {
        tiempoRestante = LapsoDeTiempo;
        base.Mostrar();
    }

}
