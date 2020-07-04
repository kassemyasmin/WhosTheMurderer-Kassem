using UnityEngine.UI;

public class Inventario : ControladorCanvas{

    private ManejadorTips manejadorTips;
	private ControladorHipotesis controladorHipotesis;												  

    protected override void CanvasStart()
    {
        manejadorTips = FindObjectOfType<ManejadorTips>();
		controladorHipotesis = GetComponent<ControladorHipotesis>();															
    }

    public override void Mostrar()
    {
        manejadorTips.ResetMemory();
        //controladorHipotesis.HideFeedback();								
        base.Mostrar();
    }
}
