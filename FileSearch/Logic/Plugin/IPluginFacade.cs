using FileSearch.Logic.UI.ViewBuilders;

namespace FileSearch.Logic.Plugin
{
    public interface IPluginFacade
    {
        Guid PluginId { get; }

        string TabTitle { get; }

        UserControl BuildTabPage();

        ICriterionPlugin[] GetCriterion();

        IViewBuilderFactory GetViewBuilderFactory();
    }
}
