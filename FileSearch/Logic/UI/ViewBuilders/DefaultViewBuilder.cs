/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.UI.Entries;

namespace FileSearch.Logic.UI.ViewBuilders
{
    internal class DefaultViewBuilder : IViewBuilder
    {
        public IEnumerable<IPathEntry> Build(Model.Engine.SearchResult entry, int entryIndex)
        {
            return new[] { new PathEntry(entry.FileSystemInfo) };
        }

        public Tuple<string, int>[] ColumnSizes
        {
            get
            {
                return new[] { new Tuple<string, int>("Сканируемые директории", -1) };
            }
        }
    }
}
