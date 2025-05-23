﻿/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.Model.Entities;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal class DateCriterion : CriterionBase, ICriterion
    {
        private readonly FileDateOption _dateOption;
        private readonly DateTime? _startDateTime;
        private readonly DateTime? _endDateTime;

        public DateCriterion(FileDateOption dateOption, DateTime? startDateTime, DateTime? endDateTime)
        {
            if (dateOption == FileDateOption.None)
                throw new ArgumentException("There is no date option specified.", "dateOption");
            if (startDateTime == null && endDateTime == null)
                throw new ArgumentException("No start and end date time are specified.");

            _dateOption = dateOption;
            _startDateTime = startDateTime != null ? (DateTime?)startDateTime.Value.ToUniversalTime() : null;
            _endDateTime = endDateTime != null ? (DateTime?)endDateTime.Value.ToUniversalTime() : null;
        }

        public string Name { get { return "Dates"; } }

        public CriterionWeight Weight { get { return CriterionWeight.Medium; } }

        public bool DirectorySupport { get { return true; } }

        public bool FileSupport { get { return true; } }

        public bool IsMatch(FileSystemInfo fileSystemInfo, ICriterionContext context)
        {
            DateTime dateTimeUtc;
            switch (_dateOption)
            {
                case FileDateOption.Accessed:
                    dateTimeUtc = fileSystemInfo.LastAccessTimeUtc;
                    break;
                case FileDateOption.Changed:
                    dateTimeUtc = fileSystemInfo.LastWriteTimeUtc;
                    break;
                case FileDateOption.Created:
                    dateTimeUtc = fileSystemInfo.CreationTimeUtc;
                    break;
                default:
                    throw new NotSupportedException();
            }

            return (_startDateTime == null || dateTimeUtc >= _startDateTime.Value)
                && (_endDateTime == null || dateTimeUtc <= _endDateTime.Value);
        }
    }
}
