/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.EncodingDetection;
using FileSearch.Logic.Model.Entities;

namespace FileSearch.Logic.Model.Engine
{
    internal class EngineOptions
    {
        public EngineOptions(DirectoryInfo[] rootDirectories)
        {
            if (rootDirectories == null) throw new ArgumentNullException("rootDirectories");
            if (rootDirectories.Length <= 0) throw new ArgumentException(@"Должен быть указан минимум 1 корневой каталог.", "rootDirectories");

            RootDirectories = rootDirectories;
        }

        public DirectoryInfo[] RootDirectories { get; private set; }

        #region Basic

        public string SearchName { get; set; }

        public bool SearchNameIgnoreCasing { get; set; }

        public bool SearchNameMatchFullPath { get; set; }

        public bool SearchNameAsRegularExpression { get; set; }

        public bool SearchRecursive { get; set; }

        public bool SearchIncludesFolders { get; set; }

        public bool SearchInArchives { get; set; }

        #endregion

        #region Attributes

        public FileAttributes AttributesIncluded { get; set; }

        public FileAttributes AttributesExcluded { get; set; }

        #endregion

        #region Size

        public long? MinimumSize { get; set; }

        public long? MaximumSize { get; set; }

        #endregion

        #region Dates

        public FileDateOption DateOption { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        #endregion

        #region File content

        public string ContentText { get; set; }

        public IEncodingFactory ContentEncodingFactory { get; set; }

        public bool ContentIgnoreCasing { get; set; }

        public bool ContentWholeWordsOnly { get; set; }

        public bool ContentAsRegularExpression { get; set; }

        public bool ContentForOfficeXml { get; set; }

        #endregion
    }
}
