﻿/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;
using FileSearch.Logic.UI.Entries;

namespace FileSearch
{
    public partial class ExceptionsForm : Form
    {
        private IList<SearchException> _exceptions;
        private SearchExceptionEntry[] _exceptionEntries;

        public ExceptionsForm()
        {
            InitializeComponent();
        }

        public void SetContent(IList<SearchException> exceptions)
        {
            if (exceptions == null) throw new ArgumentNullException("exceptions");

            _exceptions = exceptions;
            _exceptionEntries = new SearchExceptionEntry[exceptions.Count];
            lstItems.VirtualListSize = exceptions.Count;
        }

        private void lstItems_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = _exceptionEntries[e.ItemIndex];
            if (entry == null)
                _exceptionEntries[e.ItemIndex] = entry = new SearchExceptionEntry(_exceptions[e.ItemIndex]);
            e.Item = entry;
        }
    }
}
