using CodeStash.Application.Models;

namespace CodeStash.Application.Errors;
public static class SnippetErrors
{
    public static readonly Error MaximumTagsExceeded = new Error(
        "Snippet.MaximumTagsExceeded", "Maximum number of tags exceeded.");

    public static readonly Error InvalidTags = new Error(
        "Snippet.InvalidTags", "Invalid tags selected.");

    public static readonly Error CannotCreate = new Error(
        "Snippet.CannotCreate", "An error occurred while creating snippet.");

    public static readonly Error ViewRestricted = new Error(
        "Snippet.ViewRestricted", "Cannot view another user's private snippet.");

    public static readonly Error UpdateFailed = new Error(
        "Snippet.UpdateFailed", "An error occurred while updating the snippet.");

    public static readonly Error DeleteFailed = new Error(
        "Snippet.DeleteFailed", "An error occurred while deleting the snippet.");

    public static readonly Error InvalidLanguage = new Error(
        "Snippet.InvalidLanguage", "Invalid language selected.");

    public static readonly Error InvalidTitleOrContent = new Error(
        "Snippet.InvalidTitleOrContent", "Title and content are required.");

    public static readonly Error SnippetNotFound = new Error(
        "Snippet.SnippetNotFound", "The snippet could not be found.");

    public static readonly Error CannotModify = new Error(
        "Snippet.CannotModify", "Cannot modify another user's snippet.");
}
