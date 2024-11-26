using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit.Highlighting;

namespace SnippetManager
{
    public static class SyntaxHighLightingHelper
    {
        public static IHighlightingDefinition? GetSyntaxHighlighting(string language)
        {
            switch (language.ToLower())
            {
                case "c#":
                case "csharp":
                    return HighlightingManager.Instance.GetDefinition("C#");
                case "xml":
                    return HighlightingManager.Instance.GetDefinition("XML");
                case "html":
                    return HighlightingManager.Instance.GetDefinition("HTML");
                case "javascript":
                case "js":
                    return HighlightingManager.Instance.GetDefinition("JavaScript");
                case "python":
                    return HighlightingManager.Instance.GetDefinition("Python");
                case "sql":
                    return HighlightingManager.Instance.GetDefinition("SQL");
                case "java":
                    return HighlightingManager.Instance.GetDefinition("Java");
                case "css":
                    return HighlightingManager.Instance.GetDefinition("CSS");
                case "php":
                    return HighlightingManager.Instance.GetDefinition("PHP");
                case "ruby":
                    return HighlightingManager.Instance.GetDefinition("Ruby");
                case "json":
                    return HighlightingManager.Instance.GetDefinition("JSON");
                case "typescript":
                case "ts":
                    return HighlightingManager.Instance.GetDefinition("TypeScript");
                case "markdown":
                case "md":
                    return HighlightingManager.Instance.GetDefinition("Markdown");
                case "vb":
                case "visualbasic":
                    return HighlightingManager.Instance.GetDefinition("VBNET");
                default:
                    return null;
            }
        }
    }
}
