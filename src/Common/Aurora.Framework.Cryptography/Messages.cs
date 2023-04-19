namespace Aurora.Framework.Cryptography
{
    internal static class Messages
    {
        internal const string EmptyControlContentMessage = "The text of the control content cannot be empty.";
        internal const string EmptyContentToProtectMessage = "The text of the content to protect cannot be empty.";
        internal const string EmptyContentToUnprotectMessage = "The text of the content to unprotect cannot be empty.";
        internal const string InvalidControlContentMessage = "The text of the control content is invalid.";
        internal const string PasswordContentMessage = "The password text to encrypt or decrypt values cannot be empty.";
        internal const string ProtectExceptionMessage = "An exception is thrown while trying to encrypt a text content.";
        internal const string UnprotectExceptionMessage = "An exception is thrown while trying to decrypt a text content.";
    }
}