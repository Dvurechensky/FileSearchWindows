using FileSearch.Logic.UI.ViewBuilders;

namespace FileSearch.Logic.Plugin
{
    public interface IViewBuilderFactory
    {
        IViewBuilder CreateViewBuilder(ICriterionPlugin criteria);
    }
}
