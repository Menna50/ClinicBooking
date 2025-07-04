// File: ClinicBooking.Shared/ErrorCodes/AuthErrorCodes.cs

namespace ClinicBooking.Shared.ErrorCodes
{
    /// <summary>
    /// Defines constant string codes for authentication and authorization related errors.
    /// </summary>
    public static class AuthErrorCodes
    {
        public const string InvalidCredentials = "AUTH.INVALID_CREDENTIALS";
        public const string UserNotFound = "AUTH.USER_NOT_FOUND";
        public const string IncorrectPassword = "AUTH.INCORRECT_PASSWORD";
        public const string UserAlreadyExists = "AUTH.USER_ALREADY_EXISTS";
        public const string EmailAlreadyRegistered = "AUTH.EMAIL_ALREADY_REGISTERED"; // مثال جديد
        public const string UsernameAlreadyTaken = "AUTH.USERNAME_ALREADY_TAKEN";   // مثال جديد
        public const string PasswordMismatch = "AUTH.PASSWORD_MISMATCH";
        public const string TokenExpiredOrInvalid = "AUTH.TOKEN_EXPIRED_OR_INVALID";
        public const string RoleNotFound = "AUTH.ROLE_NOT_FOUND";
        public const string UserNotInRole = "AUTH.USER_NOT_IN_ROLE";
        public const string GeneralAuthError = "AUTH.GENERAL_ERROR";
    }
}
