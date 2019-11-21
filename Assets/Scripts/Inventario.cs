using UnityEngine.UI;

public class Inventario : ControladorCanvas{

    private ManejadorTips manejadorTips;

    protected override void CanvasStart()
    {
        manejadorTips = FindObjectOfType<ManejadorTips>();
    }

    public override void Mostrar()
    {
        manejadorTips.ResetMemory();
        base.Mostrar();
    }
}
