/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

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
