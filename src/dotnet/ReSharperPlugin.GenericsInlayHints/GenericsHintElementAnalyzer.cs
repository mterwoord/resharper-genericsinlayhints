using System.Linq;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using IReferenceName = JetBrains.ReSharper.Psi.CSharp.Tree.IReferenceName;

namespace ReSharperPlugin.GenericsInlayHints;

[ElementProblemAnalyzer(typeof(ITypeArgumentList),
                        HighlightingTypes = new[] { typeof(GenericsInlayHint) })]
public class GenericsHintElementAnalyzer: ElementProblemAnalyzer<ITypeArgumentList>
{
    protected override void Run(ITypeArgumentList          element,
                                ElementProblemAnalyzerData data,
                                IHighlightingConsumer      consumer)
    {
        if (element.Parent is IReferenceExpression referenceExpression)
        {
            var referencedElement = referenceExpression.Reference.Resolve().DeclaredElement as IMethod;
            if (referencedElement != null)
            {
                ProcessTypeArguments(referencedElement, element, consumer);
            }

            return;
        }

        if (element.Parent is IReferenceName parentReferenceName)
        {
            var parentReferenceNameElement = parentReferenceName.Reference.Resolve().DeclaredElement;

            if (parentReferenceNameElement != null)
            {
                if (parentReferenceNameElement is ITypeElement elementParentClassDeclaration)
                {
                    ProcessTypeArguments(elementParentClassDeclaration, element, consumer);
                    return;
                }
                
                var classDefinition = FindParentOfType<ITypeDeclaration>(element);
                var baseReferences  = classDefinition?.GetBaseDeclarationsReferences()?.ToList();
                if (baseReferences is { Count: > 0 })
                {
                    foreach (var item in baseReferences)
                    {
                        var resolved = item.Resolve();
                        var classDef = resolved.DeclaredElement as ITypeElement;
                        if (classDef == null)
                        {
                            continue;
                        }

                        if (parentReferenceNameElement.Equals(classDef))
                        {
                            ProcessTypeArguments(classDef, element, consumer);
                        }
                    }
                }
            }
        }
    }

    private static void ProcessTypeArguments(ITypeParametersOwner typeParametersOwner, ITypeArgumentList typeArgumentList, IHighlightingConsumer consumer)
    {
        foreach (var typeParam in typeParametersOwner.TypeParameters)
        {
            var typeArgumentNode = typeArgumentList.TypeArgumentNodes[typeParam.Index];

            consumer.AddHighlighting(new GenericsInlayHint(typeParam.ShortName, typeArgumentNode, typeArgumentNode.GetDocumentStartOffset(), ""));
        }
    }

    private static T FindParentOfType<T>(ITreeNode node)
        where T: ITreeNode
    {
        if (node is T result)
        {
            return result;
        }

        return FindParentOfType<T>(node.Parent);
    }
}