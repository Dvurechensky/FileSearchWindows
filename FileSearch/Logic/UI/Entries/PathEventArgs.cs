/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.UI.Entries
{
    internal class PathEventArgs : EventArgs
    {
        private readonly PathEntry _entry;

        public PathEventArgs(PathEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            _entry = entry;
        }

        public PathEntry Entry
        {
            get { return _entry; }
        }
    }
}
