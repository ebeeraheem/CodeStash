using System.Reflection;

namespace CodeStash.Core.Models;
public static class SnippetLanguage
{
    // Helper method to get all available languages
    public static List<string?> GetAll()
    {
        return typeof(SnippetLanguage)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => f.GetValue(null)?.ToString())
            .Where(value => value != null) // Filter out null values just in case
            .ToList();
    }

    // Helper method to check if a language is valid
    public static bool IsValid(string language)
    {
        return GetAll().Contains(language.ToLower());
    }

    // Special Cases
    public const string None = "none";
    public const string PlainText = "plaintext";
    public const string Unknown = "unknown";

    // Markup & Styling
    public const string HTML = "html";
    public const string XML = "xml";
    public const string CSS = "css";
    public const string SCSS = "scss";
    public const string LESS = "less";
    public const string Markdown = "markdown";
    public const string SVG = "svg";

    // JavaScript & Related
    public const string JavaScript = "javascript";
    public const string TypeScript = "typescript";
    public const string JSX = "jsx";
    public const string TSX = "tsx";
    public const string JSON = "json";
    public const string NodeJS = "nodejs";

    // Programming Languages
    public const string CSharp = "csharp";
    public const string Python = "python";
    public const string Java = "java";
    public const string Kotlin = "kotlin";
    public const string Swift = "swift";
    public const string Go = "go";
    public const string Rust = "rust";
    public const string Ruby = "ruby";
    public const string PHP = "php";
    public const string CPlusPlus = "cpp";
    public const string C = "c";
    public const string Dart = "dart";
    public const string R = "r";
    public const string Scala = "scala";
    public const string Haskell = "haskell";
    public const string Lua = "lua";
    public const string Julia = "julia";
    public const string Perl = "perl";
    public const string Assembly = "assembly";
    public const string Matlab = "matlab";
    public const string Fortran = "fortran";

    // Shell & Scripting
    public const string PowerShell = "powershell";
    public const string Bash = "bash";
    public const string Shell = "shell";
    public const string BatchFile = "batch";
    public const string Python2 = "python2";
    public const string Python3 = "python3";

    // Database
    public const string SQL = "sql";
    public const string MySQL = "mysql";
    public const string PostgreSQL = "postgresql";
    public const string MongoDB = "mongodb";
    public const string Cassandra = "cassandra";
    public const string Redis = "redis";
    public const string GraphQL = "graphql";

    // Configuration & Build
    public const string YAML = "yaml";
    public const string TOML = "toml";
    public const string Dockerfile = "dockerfile";
    public const string Docker = "docker";
    public const string Kubernetes = "kubernetes";
    public const string Terraform = "terraform";
    public const string Makefile = "makefile";
    public const string CMake = "cmake";
    public const string INI = "ini";
    public const string Properties = "properties";
    public const string HCL = "hcl";
    public const string GitIgnore = "gitignore";
    public const string EditorConfig = "editorconfig";

    // Web Assembly
    public const string WASM = "wasm";
    public const string WAT = "wat";

    // Application Specific
    public const string AppleScript = "applescript";
    public const string VBScript = "vbscript";
    public const string AutoHotkey = "autohotkey";
    public const string ActionScript = "actionscript";

    // Testing & Documentation
    public const string Gherkin = "gherkin";
    public const string JUnit = "junit";
    public const string OpenAPI = "openapi";
    public const string Swagger = "swagger";
    public const string AsciiDoc = "asciidoc";
    public const string RST = "rst";

    // Version Control
    public const string Diff = "diff";
    public const string Patch = "patch";
}
