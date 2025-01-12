using CodeStash.Application.Models;

namespace CodeStash.Application.Errors;
public static class UserErrors
{
    public static readonly Error UserNotFound = new Error(
        "User.UserNotFound", "The user could not be found.");

    public static readonly Error ProfileUpdateFailed = new Error(
        "User.ProfileUpdateFailed", "An error occurred while updating the user profile.");

    public static readonly Error PasswordResetFailed = new Error(
        "User.PasswordResetFailed", "An error occurred while resetting user password.");

    public static readonly Error EmailUpdateFailed = new Error(
        "User.EmailUpdateFailed", "An error occurred while updating the user email.");

    public static readonly Error SamePassword = new Error(
        "User.SamePassword", "New password cannot be the same as old password.");

    public static readonly Error SameStatus = new Error(
        "User.SameStatus", "Current user status is the same as the request status.");

    public static readonly Error UserNameMismatch = new Error(
        "User.UserNameMismatch", "Current username does not match the request username.");

    public static readonly Error InvalidPhoneNumber = new Error(
        "User.InvalidPhoneNumber", "Phone number must be between 10 and 20 digits and may start with a '+' sign.");

    public static readonly Error InvalidDateFormat = new Error(
        "User.InvalidDateFormat", "The provided date format is invalid.");

    public static readonly Error DateIsPast = new Error(
        "User.DateIsPast", "Suspension end date cannot be in the past.");

    public static readonly Error InvalidDateOfBirth = new Error(
        "User.InvalidDateOfBirth", "The provided date of birth is invalid.");

    public static readonly Error InvalidAge = new Error(
        "User.InvalidAge", "User must be at least 13 years old.");

    public static readonly Error NotSuspended = new Error(
        "User.NotSuspended", "Cannot set suspension end date if status is not 'Suspended'.");

    public static readonly Error UnauthorizedAccess = new Error(
        "User.UnauthorizedAccess", "You do not have permission to access this user's profile.");

    public static readonly Error WrongEndpoint = new Error(
        "User.WrongEndpoint", "Please use the role specific endpoint to proceed.");

    public static readonly Error InvalidImage = new Error(
        "User.InvalidImage", "Image is null or empty.");

    public static readonly Error NoProfilePicture = new Error(
        "User.ProfilePicture", "Failed to remove. User has no profile picture.");

    public static readonly Error DeleteFailed = new Error(
        "User.DeleteFailed", "An error occurred while deleting user.");
}
