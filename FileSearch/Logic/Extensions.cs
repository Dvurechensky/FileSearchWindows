using System.Globalization;

namespace FileSearch.Logic
{
    internal static class Extensions
    {
        public static List<string> ConvertTreeNodes(this string directoryPath)
        {
            if(directoryPath.Contains("\\"))
                return directoryPath.Split(new char[] { '\\' }).ToList();
            if (directoryPath.Contains("/"))
                return directoryPath.Split(new char[] { '/' }).ToList();
            return new List<string>();
        }

        public static string GetFriendlyNotation(this TimeSpan timeSpan)
        {
            if (timeSpan.Minutes > 2)
                return string.Format(CultureInfo.CurrentCulture, "{0:#,##0.0} мин.", timeSpan.TotalMinutes);
            return string.Format(CultureInfo.CurrentCulture, "{0:#,##0.00} сек.", timeSpan.TotalSeconds);
        }
    }
}
