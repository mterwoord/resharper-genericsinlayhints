using JetBrains.ProjectModel;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.TextControl.DocumentMarkup.Adornments;

namespace ReSharperPlugin.GenericsInlayHints;

[SolutionComponent]
public class GenericsAdornmentProvider : IHighlighterAdornmentProvider
{
    public bool IsValid(IHighlighter highlighter)
    {
        return highlighter.UserData is GenericsInlayHintBase;
    }

    public IAdornmentDataModel CreateDataModel(IHighlighter highlighter)
    {
        return highlighter.UserData is GenericsInlayHint hint
            ? new GenericsAdornmentDataModel(hint.ParameterName)
            : null;
    }
}
