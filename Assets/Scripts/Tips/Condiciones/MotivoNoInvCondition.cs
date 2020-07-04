class MotivoNoInvCondition : TipCondition
{
    private readonly ManejadorDialogos dialogos;

    public MotivoNoInvCondition(ManejadorDialogos _listaInventario) : base("MotivoNoInv")
    {
        dialogos = _listaInventario;
    }

    public override bool Evaluate()
    {
        foreach (var p in dialogos.Motivos)
            if (p.Verdadero)
                return false;
        return true;
    }
}
