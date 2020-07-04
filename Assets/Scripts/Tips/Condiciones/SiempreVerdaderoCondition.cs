
class SiempreVerdaderoCondition : TipCondition
{

    public SiempreVerdaderoCondition(string _tipConditionId) : base(_tipConditionId)
    {
    }

    public override bool Evaluate()
    {
        return true;
    }
}



