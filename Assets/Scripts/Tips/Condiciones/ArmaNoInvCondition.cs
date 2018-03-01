class ArmaNoInvCondition : TipCondition
{
    private readonly ListaInventarioUI listaInventario;

    public ArmaNoInvCondition(ListaInventarioUI _listaInventario) : base("ArmaNoInv")
    {
        listaInventario = _listaInventario;
    }

    public override bool Evaluate()
    {
        foreach (var p in listaInventario.Pistas)
            if (p.Arma)
                return false;
        return true;
    }
}

