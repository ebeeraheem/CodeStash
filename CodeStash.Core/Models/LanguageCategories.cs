using System.Reflection;

namespace CodeStash.Core.Models;

public static class LanguageCategories
{
    // Stylesheet & Presentation Languages - Used for styling and layout in web development.
    public static class StylesheetLanguages
    {
        public const string Css = "CSS";
        public const string Scss = "SCSS";
        public const string Less = "LESS";
    }

    // Data Serialization & Configuration Languages - Used for structuring and storing data, typically in a format that can be transferred across systems.
    public static class DataSerializationLanguages
    {
        public const string Json = "JSON";
    }

    // Scripting Languages - Typically interpreted languages used for automation or configuration tasks.
    public static class ScriptingLanguages
    {
        public const string PowerShell = "PowerShell";
        public const string Bash = "Bash";
        public const string Shell = "Shell";
        public const string BatchFile = "Batch File";
        public const string Python2 = "Python 2";
        public const string Python3 = "Python 3";
        public const string AppleScript = "AppleScript";
        public const string VbScript = "VBScript";
        public const string GML = "GML";
        public const string REXX = "REXX";
        public const string Groovy = "Groovy";
        public const string AutoHotkey = "AutoHotkey";
        public const string ActionScript = "ActionScript";
        public const string AngelScript = "AngelScript";
        public const string Pawn = "Pawn";
        public const string Tcl = "Tcl";
        public const string AWK = "AWK";
        public const string Sed = "Sed";
        public const string XSLT = "XSLT";
    }

    // Programming Languages - General-purpose programming languages, commonly used for building software applications.
    public static class ProgrammingLanguages
    {
        public const string Csharp = "C#";
        public const string Fsharp = "F#";
        public const string VisualBasic = "Visual Basic";
        public const string Python = "Python";
        public const string Java = "Java";
        public const string Kotlin = "Kotlin";
        public const string Swift = "Swift";
        public const string Go = "Go";
        public const string Rust = "Rust";
        public const string Ruby = "Ruby";
        public const string Solidity = "Solidity";
        public const string Ada = "Ada";
        public const string Php = "PHP";
        public const string CPlusPlus = "C++";
        public const string C = "C";
        public const string Lisp = "Lisp";
        public const string ObjectiveC = "Objective-C";
        public const string Dart = "Dart";
        public const string GdsScript = "GDScript";
        public const string R = "R";
        public const string Scala = "Scala";
        public const string Haskell = "Haskell";
        public const string Elixir = "Elixir";
        public const string Zephyr = "Zephyr";
        public const string Crystal = "Crystal";
        public const string Nim = "Nim";
        public const string Clojure = "Clojure";
        public const string Delphi = "Delphi";
        public const string Lua = "Lua";
        public const string Julia = "Julia";
        public const string Zig = "Zig";
        public const string Perl = "Perl";
        public const string Erlang = "Erlang";
        public const string Prolog = "Prolog";
        public const string Apex = "Apex";
        public const string Assembly = "Assembly";
        public const string Matlab = "Matlab";
        public const string Fortran = "Fortran";
        public const string Chapel = "Chapel";
        public const string Cobol = "Cobol";
        public const string Ocaml = "OCaml";
        public const string Forth = "Forth";
        public const string D = "D";
        public const string V = "V";
    }

    // Markup Languages - Used to define the structure and presentation of text-based information.
    public static class MarkupLanguages
    {
        public const string Html = "HTML";
        public const string Xml = "XML";
        public const string Markdown = "Markdown";
        public const string Svg = "SVG";
        public const string AsciiDoc = "AsciiDoc";
        public const string MathML = "MathML";
        public const string BBCode = "BBCode";
        public const string Rst = "RST";
        public const string Diff = "Diff";
        public const string Patch = "Patch";
    }

    // Database & Query Languages - Used for querying and interacting with databases and data storage systems.
    public static class DatabaseLanguages
    {
        public const string Sql = "SQL";
        public const string Sqlite = "SQLite";
        public const string Mysql = "MySQL";
        public const string SqlServer = "SQL Server";
        public const string Postgresql = "PostgreSQL";
        public const string Mongodb = "MongoDB";
        public const string Mariadb = "MariaDB";
        public const string Elasticsearch = "Elasticsearch";
        public const string Cassandra = "Cassandra";
        public const string Redis = "Redis";
        public const string Graphql = "GraphQL";
        public const string Datalog = "Datalog";
        public const string SPARQL = "SPARQL";
    }

    // Build & Infrastructure Languages - Used for system configuration, orchestration, and automation of infrastructure and deployment.
    public static class BuildLanguages
    {
        public const string Dockerfile = "Dockerfile";
        public const string Docker = "Docker";
        public const string Kubernetes = "Kubernetes";
        public const string Terraform = "Terraform";
        public const string Makefile = "Makefile";
        public const string CMake = "CMake";
        public const string GitIgnore = "Gitignore";
        public const string EditorConfig = "EditorConfig";
        public const string Wasm = "WASM";
        public const string Wat = "WAT";
    }

    // Configuration & Serialization Languages - Used for setting up configurations or data serialization in various formats.
    public static class ConfigurationLanguages
    {
        public const string Yaml = "YAML";
        public const string Toml = "TOML";
        public const string Ini = "INI";
        public const string Properties = "Properties";
        public const string Hcl = "HCL";
        public const string PuppetDSL = "Puppet DSL";
    }

    // Testing & Documentation Languages - Used for writing tests or documenting APIs and systems.
    public static class TestingAndDocsLanguages
    {
        public const string Junit = "JUnit";
        public const string OpenApi = "OpenAPI";
        public const string Swagger = "Swagger";
        public const string Gherkin = "Gherkin";
    }

    // Miscellaneous Languages - Special-purpose languages or formats that don't fit neatly into the above categories.
    public static class MiscellaneousLanguages
    {
        public const string Jinja = "Jinja";
        public const string IDL = "IDL";
    }

    // Helper method to get all available languages
    public static List<string?> GetAllLanguages()
    {
        return typeof(LanguageCategories)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
            .SelectMany(category => category.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => f.GetValue(null)?.ToString())
            .Where(value => value != null) // Filter out null values just in case
            .ToList();
    }

    // Helper method to get all languages for a specific category
    public static List<string?> GetCategoryLanguages(string categoryName)
    {
        // Find the category type based on the category name provided
        var categoryType = typeof(LanguageCategories).GetNestedType(
            categoryName, BindingFlags.Public | BindingFlags.Static);

        if (categoryType is null)
        {
            throw new ArgumentException($"Category '{categoryName}' does not exist.");
        }

        // Get all constant strings in the category class
        return categoryType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => f.GetValue(null)?.ToString())
            .Where(value => value != null) // Filter out null values just in case
            .ToList();
    }
}

