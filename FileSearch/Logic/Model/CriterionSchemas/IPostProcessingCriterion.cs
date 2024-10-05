using FileSearch.Logic.Model.Engine;

namespace FileSearch.Logic.Model.CriterionSchemas
{
    internal interface IPostProcessingCriterion
    {
        IEnumerable<SearchResult> PostProcess();
    }
}
