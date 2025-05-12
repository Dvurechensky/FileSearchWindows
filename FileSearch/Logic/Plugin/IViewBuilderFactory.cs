/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

using FileSearch.Logic.UI.ViewBuilders;

namespace FileSearch.Logic.Plugin
{
    public interface IViewBuilderFactory
    {
        IViewBuilder CreateViewBuilder(ICriterionPlugin criteria);
    }
}
