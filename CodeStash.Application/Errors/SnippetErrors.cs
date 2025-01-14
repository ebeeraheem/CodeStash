using CodeStash.Application.Models;

namespace CodeStash.Application.Errors;
public static class SnippetErrors
{
    public static readonly Error MaximumExceeded = new Error(
        "Snippet.MaximumExceeded", "Maximum number of tags exceeded.");

    public static readonly Error CannotCreate = new Error(
        "Snippet.CannotCreate", "An error occurred while creating snippet.");

    public static readonly Error InvalidLanguage = new Error(
        "Snippet.InvalidLanguage", "Invalid language selected.");
}
