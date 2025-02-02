using CodeStash.Core.Models;

namespace CodeStash.Application.Errors;
public static class TagErrors
{
    public static readonly Error TagNotFound = new Error(
        "Tag.TagNotFound", "The tag could not be found.");

    public static readonly Error TagAlreadyExists = new Error(
        "Tag.TagAlreadyExists", "A tag with the same name already exist.");

    public static readonly Error CannotCreate = new Error(
        "Tag.CannotCreate", "An error occurred while creating the tag.");

    public static readonly Error FailedToDelete = new Error(
        "Tag.FailedToDelete", "An error occurred while deleting the tag.");

    public static readonly Error FailedToSave = new Error(
        "Tag.FailedToSave", "An error occurred while updating the tag.");
}
