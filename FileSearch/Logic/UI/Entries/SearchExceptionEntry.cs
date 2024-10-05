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
