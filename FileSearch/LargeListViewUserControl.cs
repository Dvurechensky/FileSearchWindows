/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.UI.Entries;
using FileSearch.Logic.UI.ViewBuilders;

namespace FileSearch
{
    internal partial class LargeListViewUserControl : UserControl
    {
        public event EventHandler<PathEventArgs> FileOpened;
        public event EventHandler<PathEventArgs> DirectoryOpened;

        private readonly IList<IPathEntry> _collection;
        private ListViewItem[] _cache;
        private IViewBuilder _viewBuilder;

        public LargeListViewUserControl()
        {
            InitializeComponent();
            _collection = new List<IPathEntry>();
        }

        public int Count
        {
            get { return _cache.Length; }
        }

        public void ClearContent()
        {
            _collection.Clear();
            VirtualListChanged();
        }

        public void ResetViewBuilder()
        {
            SetViewBuilder(new DefaultViewBuilder());
        }

        public void SetViewBuilder(IViewBuilder builder)
        {
            if (_viewBuilder != null && builder.GetType() == _viewBuilder.GetType())
                return;

            _viewBuilder = builder;

            lstItems.Columns.Clear();
            foreach (var c in builder.ColumnSizes)
            {
                lstItems.Columns.Add(new ColumnHeader { Text = c.Item1, Width = c.Item2 });
            }
            LargeListViewUserControl_SizeChanged(this, EventArgs.Empty);
        }

        public void AddSearchResults(IEnumerable<SearchResult> searchResults)
        {
            if (searchResults == null) throw new ArgumentNullException("searchResults");

            int index = 0;
            var oldName = string.Empty;

            foreach (var result in searchResults)
            {
                foreach (var entry in _viewBuilder.Build(result, index))
                {
                    var directoryInfo = new DirectoryInfo(entry.FileSystemInfo.FullName);
                    if (directoryInfo.Parent?.FullName != oldName)
                    {
                        oldName = directoryInfo.Parent?.FullName;
                        _collection.Add(entry);
                    }
                }
                ++index;
            }
            VirtualListChanged();
        }

        private void VirtualListChanged()
        {
            lstItems.VirtualListSize = _collection.Count;
            _cache = new ListViewItem[lstItems.VirtualListSize];
            if (lstItems.Items.Count > 0)
                lstItems.Items[lstItems.Items.Count - 1].EnsureVisible();
        }

        private void RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = _cache[e.ItemIndex];
            if (e.Item == null)
            {
                var content = (PathEntry)_collection[e.ItemIndex];
                var item = content.BuildListViewItem();
                _cache[e.ItemIndex] = e.Item = item;
            }
        }

        private void LargeListViewUserControl_SizeChanged(object sender, EventArgs e)
        {
            if (lstItems.Columns.Count <= 0)
                return;

            var fullWidth = this.Width - SystemInformation.VerticalScrollBarWidth - SystemInformation.FrameBorderSize.Width;
            var fullColumns = lstItems.Columns.Cast<ColumnHeader>().Where((h, i) => _viewBuilder.ColumnSizes[i].Item2 == -1).ToList();

            var otherColumnWidth = 0;
            foreach (ColumnHeader c in lstItems.Columns.Cast<ColumnHeader>().Where(h => !fullColumns.Any(h2 => h2 != h)))
            {
                otherColumnWidth += c.Width;
            }

            var partWidth = fullWidth / fullColumns.Count;
            foreach (var c in fullColumns)
                c.Width = partWidth;
        }

        private void LargeListViewUserControl_Enter(object sender, EventArgs e)
        {
            lstItems.Focus();
            if (lstItems.VirtualListSize > 0 && lstItems.SelectedIndices.Count <= 0)
            {
                lstItems.SelectedIndices.Add(0);
            }
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenExplorerIfPossible();
        }

        private void OpenExplorerIfPossible()
        {
            if (lstItems.SelectedIndices.Count <= 0 || _collection.Count <= 0)
                return;

            var path = (PathEntry)(_collection[lstItems.SelectedIndices[0]]);

            if (path.IsDirectory)
            {
                if (DirectoryOpened != null)
                    DirectoryOpened(this, new PathEventArgs(path));
            }
            else if (FileOpened != null)
                FileOpened(this, new PathEventArgs(path));
        }

        private void lstItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Alt && !e.Control && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return))
            {
                OpenExplorerIfPossible();
            }
        }
    }
}
