using JetBrains.ProjectModel;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.TextControl.DocumentMarkup.IntraTextAdornments;

namespace ReSharperPlugin.GenericsInlayHints;

[SolutionComponent]
public class GenericsAdornmentProvider : IHighlighterIntraTextAdornmentProvider
{
    public bool IsValid(IHighlighter highlighter)
    {
        return highlighter.UserData is GenericsInlayHintBase;
    }

    public IIntraTextAdornmentDataModel CreateDataModel(IHighlighter highlighter)
    {
        return highlighter.UserData is GenericsInlayHint hint
            ? new GenericsAdornmentDataModel(hint.ParameterName)
            : null;
    }
}
