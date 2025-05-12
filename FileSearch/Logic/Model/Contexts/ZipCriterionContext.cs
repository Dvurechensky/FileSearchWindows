/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.Model.Engine;
using System.Collections.ObjectModel;

namespace FileSearch.Logic.Model.Contexts
{
    internal sealed class ZipCriterionContext : ICriterionContext
    {
        public ZipCriterionContext()
        {
            this.Childs = new Collection<string>();
        }

        public IList<string> Childs { get; private set; }
    }
}
