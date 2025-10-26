namespace Api.Constants
{
    public static class ErrorMessages
    {
        public const string JwtSecretKeyNotFound = "JWT secret key is missing in configuration.";
        public const string InvalidCredentials = "Invalid username or password.";
        public const string InvalidTokenType = "Invalid token type.";
        public const string UpdateFailed = "The record has changed or does not exist.";
    }
}
