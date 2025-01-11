using CodeStash.Application.Models;

namespace CodeStash.Application.Errors;
public static class AuthErrors
{
    public static readonly Error UserNotFound = new Error(
        "Auth.UserNotFound", "The user could not be found.");

    public static readonly Error RegistrationFailed = new Error(
        "Auth.RegistrationFailed", "Error creating user.");

    public static readonly Error LoginFailed = new Error(
        "Auth.LoginFailed", "Invalid credentials provided.");

    public static readonly Error EmailExist = new Error(
        "Auth.EmailExist", "Email address already exists.");

    public static readonly Error EmailAlreadyConfirmed = new Error(
        "Auth.EmailAlreadyConfirmed", "The email address has already been confirmed.");

    public static readonly Error EmailConfirmationFailed = new Error(
        "Auth.EmailConfirmationFailed", "Invalid or expired token provided.");

    public static readonly Error UserNameExist = new Error(
        "Auth.UserNameExist", "Username is taken.");

    public static readonly Error InvalidUserName = new Error(
        "Auth.InvalidUserName", "Username can only contain alphanumeric characters and an underscore.");

    public static readonly Error InvalidRole = new Error(
        "Auth.InvalidRole", "Role does not exist.");

    public static readonly Error InvalidToken = new Error(
        "Auth.InvalidToken", "Invalid or expired refresh token.");

    public static readonly Error CannotCreateUser = new Error(
        "Auth.CannotCreateUser", "An error occuurred while creating user.");

    public static readonly Error CannotAddToRole = new Error(
        "Auth.CannotAddToRole", "Failed to add user to role.");

    public static readonly Error UserLockedOut = new Error(
        "Auth.UserLockedOut", "The account is locked due to multiple failed login attempts.");
}
