/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch.Logic.UI.ViewBuilders
{
    internal static class ViewBuilderFactory
    {
        public static IViewBuilder Create()
        {
            IViewBuilder builder = null;

            return builder ?? new DefaultViewBuilder();
        }
    }
}
