using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.Model.Entities;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal static class CriteriaFactory
    {
        public static IList<ICriterion> Build(EngineOptions options)
        {
            var list = new List<ICriterion>();

            // Применить базовые параметры
            if (!string.IsNullOrEmpty(options.SearchName))
            {
                if (options.SearchInArchives)
                    list.Add(new NameAndZipCriterion(options.SearchName, options.SearchNameIgnoreCasing, options.SearchNameMatchFullPath));
                else if (!options.SearchNameAsRegularExpression)
                    list.Add(new NameCriterion(options.SearchName, options.SearchNameIgnoreCasing, options.SearchNameMatchFullPath));
                else
                    list.Add(new NameRegexCriterion(options.SearchName, options.SearchNameIgnoreCasing, options.SearchNameMatchFullPath));
            }

            // Добавить атрибуты файла
            if (options.AttributesIncluded > 0 || options.AttributesExcluded > 0)
            {
                list.Add(new AttributeCriterion(options.AttributesIncluded, options.AttributesExcluded));
            }

            // Размеры
            if (options.MinimumSize != null || options.MaximumSize != null)
            {
                list.Add(new SizeCriterion(options.MinimumSize, options.MaximumSize));
            }

            // Даты
            if (options.DateOption != FileDateOption.None && (options.StartDateTime != null || options.EndDateTime != null))
            {
                list.Add(new DateCriterion(options.DateOption, options.StartDateTime, options.EndDateTime));
            }

            // Содержание
            if (!string.IsNullOrEmpty(options.ContentText))
            {
                if (!options.ContentAsRegularExpression)
                    list.Add(new ContentCriterion(options.ContentText, options.ContentIgnoreCasing, options.ContentWholeWordsOnly, options.ContentEncodingFactory));
                else
                    list.Add(new ContentRegexCriterion(options.ContentText, options.ContentIgnoreCasing));
            }

            return list.ToArray();
        }
    }
}
