namespace FileSearch.Logic.Model.Engine
{
    public class CriterionBase
    {
        public virtual ICriterionContext BuildContext()
        {
            return null;
        }
    }

    public class CriterionBase<TContext> : CriterionBase where TContext : ICriterionContext, new()
    {
        public override ICriterionContext BuildContext()
        {
            return new TContext();
        }
    }
}
