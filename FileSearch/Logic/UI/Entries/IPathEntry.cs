namespace FileSearch.Logic.UI.Entries
{
    public interface IPathEntry
    {
        FileSystemInfo FileSystemInfo { get; }

        bool IsDirectory { get; }

        ListViewItem BuildListViewItem();
    }
}
