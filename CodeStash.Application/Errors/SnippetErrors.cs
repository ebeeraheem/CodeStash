using CodeStash.Application.Models;

namespace CodeStash.Application.Errors;
public static class SnippetErrors
{
    public static readonly Error MaximumTagsExceeded = new Error(
        "Snippet.MaximumTagsExceeded", "Maximum number of tags exceeded.");

    public static readonly Error CannotCreate = new Error(
        "Snippet.CannotCreate", "An error occurred while creating snippet.");

    public static readonly Error UpdateFailed = new Error(
        "Snippet.UpdateFailed", "An error occurred while updating the snippet.");

    public static readonly Error InvalidLanguage = new Error(
        "Snippet.InvalidLanguage", "Invalid language selected.");

    public static readonly Error SnippetNotFound = new Error(
        "Snippet.SnippetNotFound", "The snippet could not be found.");
}
