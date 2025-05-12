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
    internal class SizeCriterion : CriterionBase, ICriterion
    {
        private readonly long? _minLengthInBytes;
        private readonly long? _maxLengthInBytes;

        public SizeCriterion(long? minLengthInBytes, long? maxLengthInBytes)
        {
            if (minLengthInBytes != null && minLengthInBytes < 0)
                throw new ArgumentOutOfRangeException("minLengthInBytes", "Указанная минимальная длина не может быть меньше 0 байт.");
            if (maxLengthInBytes != null && maxLengthInBytes < 0)
                throw new ArgumentOutOfRangeException("maxLengthInBytes", "Указанная максимальная длина не может быть меньше 0 байт.");
            if (minLengthInBytes == null && maxLengthInBytes == null)
                throw new ArgumentException("Невозможно установить как минимальное, так и максимальное значения равными нулю.");
            if (minLengthInBytes != null && maxLengthInBytes != null && minLengthInBytes > maxLengthInBytes)
                throw new ArgumentException("Максимальное значение должно быть больше или равно минимальному.");

            _minLengthInBytes = minLengthInBytes;
            _maxLengthInBytes = maxLengthInBytes;
        }

        public string Name { get { return "File sizes"; } }

        public CriterionWeight Weight { get { return CriterionWeight.Medium; } }

        public bool DirectorySupport { get { return false; } }

        public bool FileSupport { get { return true; } }

        public bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            var file = (FileInfo)fileSystemInfo;
            return (_minLengthInBytes == null || file.Length >= _minLengthInBytes.Value)
                && (_maxLengthInBytes == null || file.Length <= _maxLengthInBytes.Value);
        }
    }
}
