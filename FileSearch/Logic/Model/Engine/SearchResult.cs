/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.Model.Engine
{
    public class SearchResult
    {
        public SearchResult(FileSystemInfo fileSystemInfo)
        {
            this.FileSystemInfo = fileSystemInfo;
        }

        /// <summary>
        /// Получает файл или каталог для данного результата поиска.
        /// </summary>
        public FileSystemInfo FileSystemInfo { get; private set; }

        /// <summary>
        /// Получает или устанавливает коллекцию со всеми метаданными для этого результата поиска.
        /// Тип критерия, задавшего контекст.
        /// </summary>
        public IDictionary<Type, ICriterionContext> Metadata { get; set; }
    }
}
