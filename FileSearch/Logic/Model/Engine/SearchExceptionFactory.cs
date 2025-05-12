/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using System.Xml;
using Ionic.Zip;

namespace FileSearch.Logic.Model.Engine
{
    internal static class SearchExceptionFactory
    {
        public static SearchException Build(FileSystemInfo fileSystemInfo, Exception originalException)
        {
            if (originalException is PathTooLongException)
                return new SearchException(fileSystemInfo, originalException, "Путь слишком длинный.");

            if (originalException is IOException)
                return new SearchException(fileSystemInfo, originalException, "Файл заблокирован для чтения.");

            if (originalException is UnauthorizedAccessException)
                return new SearchException(fileSystemInfo, originalException, "Недостаточно прав на чтение файла или папки.");

            if (originalException is XmlException)
                return new SearchException(fileSystemInfo, originalException, "XML содержит недопустимые символы.");

            if (originalException is ZipException)
                return new SearchException(fileSystemInfo, originalException, "Невозможно обработать ZIP-файл.");

            return new SearchException(fileSystemInfo, originalException, "Неизвестное необработанное исключение.");
        }
    }
}
