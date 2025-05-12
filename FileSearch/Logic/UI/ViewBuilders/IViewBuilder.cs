/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

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
