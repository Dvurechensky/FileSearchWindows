/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;

namespace FileSearch.Logic.UI.Entries
{
    internal sealed class SearchExceptionEntry : ListViewItem
    {
        private readonly SearchException _exception;

        public SearchExceptionEntry(SearchException exception)
        {
            _exception = exception;

            this.Text = exception.FileSystemInfo.FullName;
            this.SubItems.Add(exception.FriendlyDescription);
        }

        public SearchException Exception
        {
            get { return _exception; }
        }
    }
}
