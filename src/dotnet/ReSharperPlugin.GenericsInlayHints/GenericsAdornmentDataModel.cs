using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.Application.UI.Controls.Utils;
using JetBrains.Application.UI.PopupLayout;
using JetBrains.TextControl.DocumentMarkup.IntraTextAdornments;
using JetBrains.UI.Icons;
using JetBrains.UI.RichText;
using JetBrains.Util;

namespace ReSharperPlugin.GenericsInlayHints;

[PublicAPI]
public class GenericsAdornmentDataModel : IIntraTextAdornmentDataModel
{
    public GenericsAdornmentDataModel(string parameterName)
    {
        Text = parameterName + ":";
    }

    public void ExecuteNavigation(PopupWindowContextSource popupWindowContextSource)
    {
        MessageBox.ShowInfo($"{nameof(GenericsAdornmentDataModel)}.{nameof(ExecuteNavigation)}", "ReSharper SDK");
    }

    public IntraTextAdornmentData Data
    {
        get
        {
            var result = new IntraTextAdornmentData(Text, IconId, IntraTextAdornmentFlags.IsNavigable, IntraTextPlacement.DefaultBeforeThisChar, InlayHintsMode);
            return result;
        }
}

    public RichText Text { get; }
    public IPresentableItem ContextMenuTitle { get; }
    public IEnumerable<BulbMenuItem> ContextMenuItems { get; }
    public TextRange? SelectionRange { get; }
    public IconId IconId { get; }
    public InlayHintsMode InlayHintsMode => InlayHintsMode.Always;
}
