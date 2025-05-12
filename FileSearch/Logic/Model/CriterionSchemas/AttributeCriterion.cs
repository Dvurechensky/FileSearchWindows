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
    internal class AttributeCriterion : CriterionBase, ICriterion
    {
        private readonly FileAttributes _includedAttributes;
        private readonly FileAttributes _excludedAttributes;

        public AttributeCriterion(FileAttributes includedAttributes, FileAttributes excludedAttributes)
        {
            _includedAttributes = includedAttributes;
            _excludedAttributes = excludedAttributes;
        }

        public string Name { get { return "Attributes"; } }

        public CriterionWeight Weight { get { return CriterionWeight.Medium; } }

        public bool DirectorySupport { get { return true; } }

        public bool FileSupport { get { return true; } }

        public bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            return (fileSystemInfo.Attributes & _includedAttributes) == _includedAttributes
                && (fileSystemInfo.Attributes & _excludedAttributes) == 0;
        }
    }
}
