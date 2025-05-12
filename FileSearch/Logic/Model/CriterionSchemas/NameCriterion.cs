/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;
using System.Text.RegularExpressions;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal class NameCriterion : CriterionBase, ICriterion
    {
        private static readonly string StarWildcard = Regex.Escape("*");
        private static readonly string QuestionWildcard = Regex.Escape("?");

        private string[] _exactMatches;
        private Regex[] _regexMatches;

        private readonly bool _ignoreCasing;
        protected readonly bool MatchFullPath;

        public NameCriterion(string value, bool ignoreCasing, bool matchFullPath)
        {
            if (value == null) throw new ArgumentNullException("value");

            _ignoreCasing = ignoreCasing;
            this.MatchFullPath = matchFullPath;
            BuildMatches(value);
        }

        public string Name { get { return "File and directory names"; } }

        public virtual CriterionWeight Weight
        {
            get { return CriterionWeight.None; }
        }

        public virtual bool DirectorySupport
        {
            get { return true; }
        }

        public virtual bool FileSupport
        {
            get { return true; }
        }

        public virtual bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            var name = this.MatchFullPath ? fileSystemInfo.FullName : fileSystemInfo.Name;
            return IsMatch(name);
        }

        protected bool IsMatch(string fileName)
        {
            if (_exactMatches.Length > 0 && _exactMatches.Any(m => SimpleMatch(fileName, m)))
                return true;
            if (_regexMatches.Length > 0 && _regexMatches.Any(m => WildcardMatch(fileName, m)))
                return true;
            return false;
        }

        private bool SimpleMatch(string filePath, string value)
        {
            return filePath.IndexOf(value, _ignoreCasing ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
        }

        private static bool WildcardMatch(string filePath, Regex value)
        {
            return value.IsMatch(filePath);
        }

        private void BuildMatches(string value)
        {
            var exactMatches = new List<string>();
            var regexMatches = new List<Regex>();

            var split = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in split)
            {
                // Build regex for a like statement
                if (item.Contains("*") || item.Contains("?"))
                {
                    var valueRegex = Regex.Escape(item)
                       .Replace(QuestionWildcard, ".{1}")
                       .Replace(StarWildcard, ".*");

                    regexMatches.Add(new Regex(string.Concat("^", valueRegex, "$"), RegexOptions.Compiled | (_ignoreCasing ? RegexOptions.IgnoreCase : RegexOptions.None)));
                }
                else
                    exactMatches.Add(item);
            }

            _exactMatches = exactMatches.ToArray();
            _regexMatches = regexMatches.ToArray();
        }
    }
}
