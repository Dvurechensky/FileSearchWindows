/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal class ContentRegexCriterion : CriterionBase, ICriterion
    {
        private readonly string _regexText;
        private readonly bool _ignoreCase;

        public ContentRegexCriterion(string regexText, bool ignoreCase)
        {
            if (regexText == null) throw new ArgumentNullException("regexText");

            _regexText = regexText;
            _ignoreCase = ignoreCase;
        }

        public string Name { get { return "File content using regular expressions"; } }

        public CriterionWeight Weight { get { return CriterionWeight.Extreme; } }

        public bool DirectorySupport { get { return false; } }

        public bool FileSupport { get { return true; } }

        public bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            var fileInfo = (FileInfo)fileSystemInfo;
            throw new NotImplementedException();
        }
    }
}
