using ICSharpCode.AvalonEdit.Highlighting;

namespace SnippetManager;

public static class SyntaxHighLightingHelper
{
    public static IHighlightingDefinition? GetSyntaxHighlighting(string language)
    {
        switch (language.ToLower())
        {
            case "asp":
            case "xhtml":
                return HighlightingManager.Instance.GetDefinition("ASP/XHTML");
            case "boo":
                return HighlightingManager.Instance.GetDefinition("Boo");
            case "coco":
                return HighlightingManager.Instance.GetDefinition("Coco");
            case "css":
                return HighlightingManager.Instance.GetDefinition("CSS");
            case "c++":
            case "cpp":
            case "cplusplus":
                return HighlightingManager.Instance.GetDefinition("C++");
            case "c#":
            case "csharp":
                return HighlightingManager.Instance.GetDefinition("C#");
            case "html":
                return HighlightingManager.Instance.GetDefinition("HTML");
            case "java":
                return HighlightingManager.Instance.GetDefinition("Java");
            case "javascript":
            case "js":
                return HighlightingManager.Instance.GetDefinition("JavaScript");
            case "patch":
            case "diff":
                return HighlightingManager.Instance.GetDefinition("Patch");
            case "php":
                return HighlightingManager.Instance.GetDefinition("PHP");
            case "powershell":
            case "ps":
                return HighlightingManager.Instance.GetDefinition("PowerShell");
            case "tex":
            case "latex":
                return HighlightingManager.Instance.GetDefinition("TeX");
            case "vb":
            case "visualbasic":
            case "vbnet":
                return HighlightingManager.Instance.GetDefinition("VBNET");
            case "xml":
                return HighlightingManager.Instance.GetDefinition("XML");
            case "xmldoc":
                return HighlightingManager.Instance.GetDefinition("XmlDoc");
            default:
                return null;
        }
    }
}
