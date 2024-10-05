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
