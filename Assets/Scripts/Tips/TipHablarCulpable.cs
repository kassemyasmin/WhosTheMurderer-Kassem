using System.Linq;

public class TipHablarCulpable : TipCondicional
{
    private ColeccionSospechosos sospechosos;

    protected override void Start()
    {
        base.Start();

        sospechosos = FindObjectOfType<ColeccionSospechosos>();
    }

    protected override bool Hecho()
    {
        return sospechosos.SospechososEncontrados.Values.Any(sospechoso => sospechoso.Culpable);
    }
}