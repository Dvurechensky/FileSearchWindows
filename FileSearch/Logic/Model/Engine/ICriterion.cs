namespace FileSearch.Logic.Model.Engine
{
    public interface ICriterion
    {
        /// <summary>
        /// Имя критерия фильтра.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Значение, указывающее усилия, необходимые системе для сопоставления файла.
        /// Чем выше число, тем позже проверяется критерий.
        /// </summary>
        CriterionWeight Weight { get; }

        /// <summary>
        /// Указывает, поддерживает ли этот экземпляр критерия каталоги.
        /// </summary>
        bool DirectorySupport { get; }

        /// <summary>
        /// Указывает, поддерживает ли этот экземпляр критерия файлы.
        /// </summary>
        bool FileSupport { get; }

        /// <summary>
        /// Проверяет, соответствует ли файл или каталог этому критерию.
        /// </summary>
        /// <param name="fileSystemInfo">Entry файловой системы.</param>
        /// <param name="context">Контекст текущей работы всех критериев.</param>
        /// <returns></returns>
        bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context);

        /// <summary>
        /// Создает новый контекст для этого критерия.
        /// </summary>
        /// <returns>Новый контекст для сопоставления файлов.</returns>
        ICriterionContext BuildContext();
    }
}
