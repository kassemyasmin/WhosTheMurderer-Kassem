public abstract class TipCondition
{
    public TipCondition(string _tipConditionId)
    {
        TipConditionId = _tipConditionId;
    }

    public string TipConditionId { get; private set; }
    public abstract bool Evaluate();
}

