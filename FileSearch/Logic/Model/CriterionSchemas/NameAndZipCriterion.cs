using FileSearch.Logic.Model.Contexts;
using FileSearch.Logic.Model.Engine;
using Ionic.Zip;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal class NameAndZipCriterion : NameCriterion
    {
        public NameAndZipCriterion(string value, bool ignoreCasing, bool matchFullPath)
            : base(value, ignoreCasing, matchFullPath)
        {
        }

        public override CriterionWeight Weight
        {
            get { return CriterionWeight.Light; }
        }

        public override bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            // базовое сравнение
            if (base.IsMatch(fileSystemInfo, context))
                return true;

            // Должно заканчиваться на .ZIP.
            if (string.IsNullOrEmpty(fileSystemInfo.Extension) || !fileSystemInfo.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
                return false;

            // Все найденные записи в ZIP-файле.
            var myContext = (ZipCriterionContext)context;

            using (var zip = ZipFile.Read(fileSystemInfo.FullName))
            {
                foreach (var entry in zip.EntryFileNames.Select(e => e.Replace('/', '\\')))
                {
                    if (IsMatch(this.MatchFullPath ? Path.Combine(fileSystemInfo.FullName, entry) : Path.GetFileName(entry)))
                    {
                        myContext.Childs.Add(entry);
                    }
                }
            }

            return myContext.Childs.Count > 0;
        }

        public override ICriterionContext BuildContext()
        {
            return new ZipCriterionContext();
        }
    }
}
