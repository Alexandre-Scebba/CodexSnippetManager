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
                // General-purpose and popular programming languages
                case "c#":
                case "csharp":
                    return HighlightingManager.Instance.GetDefinition("C#");
                case "c++":
                case "cpp":
                case "cplusplus":
                    return HighlightingManager.Instance.GetDefinition("C++");
                case "c":
                    return HighlightingManager.Instance.GetDefinition("C");
                case "java":
                    return HighlightingManager.Instance.GetDefinition("Java");
                case "python":
                    return HighlightingManager.Instance.GetDefinition("Python");
                case "javascript":
                case "js":
                    return HighlightingManager.Instance.GetDefinition("JavaScript");
                case "typescript":
                case "ts":
                    return HighlightingManager.Instance.GetDefinition("TypeScript");
                case "go":
                case "golang":
                    return HighlightingManager.Instance.GetDefinition("Go");
                case "swift":
                    return HighlightingManager.Instance.GetDefinition("Swift");
                case "kotlin":
                    return HighlightingManager.Instance.GetDefinition("Kotlin");
                case "php":
                    return HighlightingManager.Instance.GetDefinition("PHP");
                case "ruby":
                    return HighlightingManager.Instance.GetDefinition("Ruby");
                case "perl":
                    return HighlightingManager.Instance.GetDefinition("Perl");
                case "rust":
                    return HighlightingManager.Instance.GetDefinition("Rust");
                case "r":
                    return HighlightingManager.Instance.GetDefinition("R");
                case "haskell":
                    return HighlightingManager.Instance.GetDefinition("Haskell");
                case "scala":
                    return HighlightingManager.Instance.GetDefinition("Scala");
                case "lua":
                    return HighlightingManager.Instance.GetDefinition("Lua");
                case "vb":
                case "visualbasic":
                case "vbnet":
                    return HighlightingManager.Instance.GetDefinition("VBNET");
                case "fsharp":
                case "f#":
                    return HighlightingManager.Instance.GetDefinition("F#");
                case "objective-c":
                case "objc":
                case "objectivec":
                    return HighlightingManager.Instance.GetDefinition("Objective-C");
                case "shell":
                case "bash":
                case "sh":
                    return HighlightingManager.Instance.GetDefinition("Bash");
                case "powershell":
                case "ps":
                    return HighlightingManager.Instance.GetDefinition("PowerShell");
                case "matlab":
                    return HighlightingManager.Instance.GetDefinition("MATLAB");
                case "groovy":
                    return HighlightingManager.Instance.GetDefinition("Groovy");
                case "delphi":
                    return HighlightingManager.Instance.GetDefinition("Delphi");
                case "ada":
                    return HighlightingManager.Instance.GetDefinition("Ada");
                case "fortran":
                    return HighlightingManager.Instance.GetDefinition("Fortran");

                // Markup, query, and scripting languages
                case "html":
                    return HighlightingManager.Instance.GetDefinition("HTML");
                case "xml":
                    return HighlightingManager.Instance.GetDefinition("XML");
                case "yaml":
                case "yml":
                    return HighlightingManager.Instance.GetDefinition("YAML");
                case "json":
                    return HighlightingManager.Instance.GetDefinition("JSON");
                case "sql":
                    return HighlightingManager.Instance.GetDefinition("SQL");
                case "graphql":
                    return HighlightingManager.Instance.GetDefinition("GraphQL");
                case "css":
                    return HighlightingManager.Instance.GetDefinition("CSS");
                case "sass":
                case "scss":
                    return HighlightingManager.Instance.GetDefinition("Sass");
                case "markdown":
                case "md":
                    return HighlightingManager.Instance.GetDefinition("Markdown");
                case "ini":
                    return HighlightingManager.Instance.GetDefinition("INI");
                case "csv":
                    return HighlightingManager.Instance.GetDefinition("CSV");

                // Historical and niche languages
                case "pascal":
                    return HighlightingManager.Instance.GetDefinition("Pascal");
                case "cobol":
                    return HighlightingManager.Instance.GetDefinition("COBOL");
                case "lisp":
                case "commonlisp":
                    return HighlightingManager.Instance.GetDefinition("Lisp");
                case "scheme":
                    return HighlightingManager.Instance.GetDefinition("Scheme");
                case "forth":
                    return HighlightingManager.Instance.GetDefinition("Forth");
                case "algol":
                case "algol60":
                case "algol68":
                    return HighlightingManager.Instance.GetDefinition("ALGOL");
                case "smalltalk":
                    return HighlightingManager.Instance.GetDefinition("Smalltalk");
                case "modula-2":
                case "modula":
                    return HighlightingManager.Instance.GetDefinition("Modula-2");

                // Game development and graphics scripting languages
                case "gml":
                    return HighlightingManager.Instance.GetDefinition("GameMaker Language");
                case "povray":
                case "pov":
                    return HighlightingManager.Instance.GetDefinition("POV-Ray");
                case "glsl":
                    return HighlightingManager.Instance.GetDefinition("GLSL");
                case "hlsl":
                    return HighlightingManager.Instance.GetDefinition("HLSL");
                case "godot":
                case "gdscript":
                    return HighlightingManager.Instance.GetDefinition("GDScript");

                // Configuration and data serialization languages
                case "dockerfile":
                case "docker":
                    return HighlightingManager.Instance.GetDefinition("Dockerfile");
                case "toml":
                    return HighlightingManager.Instance.GetDefinition("TOML");
                case "protobuf":
                case "proto":
                    return HighlightingManager.Instance.GetDefinition("Protocol Buffers");

                // More popular languages
                case "clojure":
                    return HighlightingManager.Instance.GetDefinition("Clojure");
                case "erlang":
                    return HighlightingManager.Instance.GetDefinition("Erlang");
                case "elixir":
                    return HighlightingManager.Instance.GetDefinition("Elixir");
                case "nim":
                    return HighlightingManager.Instance.GetDefinition("Nim");
                case "crystal":
                    return HighlightingManager.Instance.GetDefinition("Crystal");
                case "tcl":
                    return HighlightingManager.Instance.GetDefinition("Tcl");
                case "coffeescript":
                case "coffee":
                    return HighlightingManager.Instance.GetDefinition("CoffeeScript");
                case "red":
                    return HighlightingManager.Instance.GetDefinition("Red");
                case "julia":
                    return HighlightingManager.Instance.GetDefinition("Julia");
                case "elm":
                    return HighlightingManager.Instance.GetDefinition("Elm");
                case "haxe":
                    return HighlightingManager.Instance.GetDefinition("Haxe");
                case "janet":
                    return HighlightingManager.Instance.GetDefinition("Janet");
                case "zig":
                    return HighlightingManager.Instance.GetDefinition("Zig");

                // Specialized and domain-specific languages
                case "vhdl":
                    return HighlightingManager.Instance.GetDefinition("VHDL");
                case "verilog":
                    return HighlightingManager.Instance.GetDefinition("Verilog");
                case "abap":
                    return HighlightingManager.Instance.GetDefinition("ABAP");
                case "puppet":
                    return HighlightingManager.Instance.GetDefinition("Puppet");
                case "ansible":
                    return HighlightingManager.Instance.GetDefinition("Ansible");
                case "solidity":
                    return HighlightingManager.Instance.GetDefinition("Solidity");
                case "purebasic":
                    return HighlightingManager.Instance.GetDefinition("PureBasic");
                case "xslt":
                    return HighlightingManager.Instance.GetDefinition("XSLT");
                case "dot":
                case "graphviz":
                    return HighlightingManager.Instance.GetDefinition("DOT");
                case "capnproto":
                case "capnp":
                    return HighlightingManager.Instance.GetDefinition("Cap'n Proto");
                case "gherkin":
                    return HighlightingManager.Instance.GetDefinition("Gherkin");
                case "maxscript":
                    return HighlightingManager.Instance.GetDefinition("MaxScript");
                case "eiffel":
                    return HighlightingManager.Instance.GetDefinition("Eiffel");

                // Assembly and machine code
                case "nasm":
                case "asm":
                case "assembly":
                    return HighlightingManager.Instance.GetDefinition("Assembly");

                // Functional languages
                case "ocaml":
                    return HighlightingManager.Instance.GetDefinition("OCaml");
                case "racket":
                    return HighlightingManager.Instance.GetDefinition("Racket");
                case "sml":
                case "standardml":
                    return HighlightingManager.Instance.GetDefinition("Standard ML");

                // Logic and constraint programming languages
                case "prolog":
                    return HighlightingManager.Instance.GetDefinition("Prolog");
                case "mercury":
                    return HighlightingManager.Instance.GetDefinition("Mercury");

                // Experimental and new languages
                case "bosque":
                    return HighlightingManager.Instance.GetDefinition("Bosque");
                case "gleam":
                    return HighlightingManager.Instance.GetDefinition("Gleam");
                case "grain":
                    return HighlightingManager.Instance.GetDefinition("Grain");
                case "mint":
                    return HighlightingManager.Instance.GetDefinition("Mint");

                // High-level scripting and automation languages
                case "autohotkey":
                case "ahk":
                    return HighlightingManager.Instance.GetDefinition("AutoHotkey");
                case "autoit":
                    return HighlightingManager.Instance.GetDefinition("AutoIt");
                case "applescript":
                    return HighlightingManager.Instance.GetDefinition("AppleScript");
                case "sed":
                    return HighlightingManager.Instance.GetDefinition("Sed");
                case "awk":
                    return HighlightingManager.Instance.GetDefinition("AWK");

                // Legacy and obscure languages
                case "bcpl":
                    return HighlightingManager.Instance.GetDefinition("BCPL");
                case "apl":
                    return HighlightingManager.Instance.GetDefinition("APL");
                case "euphoria":
                    return HighlightingManager.Instance.GetDefinition("Euphoria");
                case "mirah":
                    return HighlightingManager.Instance.GetDefinition("Mirah");
                case "nemerle":
                    return HighlightingManager.Instance.GetDefinition("Nemerle");
                case "rexx":
                    return HighlightingManager.Instance.GetDefinition("REXX");
                case "bliss":
                    return HighlightingManager.Instance.GetDefinition("BLISS");
                case "blitzmax":
                    return HighlightingManager.Instance.GetDefinition("BlitzMax");

                // Numeric and scientific languages
                case "rpl":
                    return HighlightingManager.Instance.GetDefinition("RPL");
                case "spice":
                    return HighlightingManager.Instance.GetDefinition("SPICE");

                // Machine learning and statistical languages
                case "stan":
                    return HighlightingManager.Instance.GetDefinition("Stan");
                case "greta":
                    return HighlightingManager.Instance.GetDefinition("Greta");

                // Web-related and templating languages
                case "liquid":
                    return HighlightingManager.Instance.GetDefinition("Liquid");
                case "haml":
                    return HighlightingManager.Instance.GetDefinition("Haml");
                case "twig":
                    return HighlightingManager.Instance.GetDefinition("Twig");
                case "mustache":
                    return HighlightingManager.Instance.GetDefinition("Mustache");

                // Declarative and markup languages
                case "latex":
                case "tex":
                    return HighlightingManager.Instance.GetDefinition("LaTeX");

                // Blockchain and smart contract languages
                case "vyper":
                    return HighlightingManager.Instance.GetDefinition("Vyper");

                // Miscellaneous
                case "promela":
                    return HighlightingManager.Instance.GetDefinition("Promela");
                case "picat":
                    return HighlightingManager.Instance.GetDefinition("Picat");
                case "rebol":
                    return HighlightingManager.Instance.GetDefinition("Rebol");
                case "riot":
                    return HighlightingManager.Instance.GetDefinition("Riot");

                default:
                    return null;
            }
        }
    }
}
