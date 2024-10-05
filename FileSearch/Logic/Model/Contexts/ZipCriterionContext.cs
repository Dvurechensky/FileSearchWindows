using FileSearch.Logic.Model.Engine;
using System.Collections.ObjectModel;

namespace FileSearch.Logic.Model.Contexts
{
    internal sealed class ZipCriterionContext : ICriterionContext
    {
        public ZipCriterionContext()
        {
            this.Childs = new Collection<string>();
        }

        public IList<string> Childs { get; private set; }
    }
}
