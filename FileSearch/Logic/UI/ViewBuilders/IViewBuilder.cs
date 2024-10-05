using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.UI.Entries;

namespace FileSearch.Logic.UI.ViewBuilders
{
    public interface IViewBuilder
    {
        IEnumerable<IPathEntry> Build(SearchResult entry, int entryIndex);

        Tuple<string, int>[] ColumnSizes { get; }
    }
}
