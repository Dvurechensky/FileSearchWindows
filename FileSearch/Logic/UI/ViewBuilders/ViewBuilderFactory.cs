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
