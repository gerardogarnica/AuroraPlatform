using Aurora.Framework.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Framework.Cryptography
{
    public static class SymmetricEncryptionProvider
    {
        public static Tuple<string, string> Protect(string content, string password)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new PlatformException(Messages.EmptyContentToProtectMessage);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new PlatformException(Messages.PasswordContentMessage);
            }

            try
            {
                // Create control data bytes
                var symmetricKey = new SymmetricKey(password);
                var rawControlData = symmetricKey.ToToken();

                // Get bytes from content
                var rawContent = Encoding.UTF8.GetBytes(content);

                // Encrypt the content
                using var aes = SymmetricCryptography.CreateAesProvider(symmetricKey);
                using var memoryStream = new MemoryStream();
                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(rawContent, 0, rawContent.Length);
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();

                // Returns the encrypted content
                var encryptedContent = Convert.ToBase64String(memoryStream.ToArray());
                var controlContent = Convert.ToBase64String(rawControlData);

                return Tuple.Create(encryptedContent, controlContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(Messages.ProtectExceptionMessage, e);
            }
        }

        public static string Unprotect(string content, string controlContent, string password)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new PlatformException(Messages.EmptyContentToUnprotectMessage);
            }

            if (string.IsNullOrWhiteSpace(controlContent))
            {
                throw new PlatformException(Messages.EmptyControlContentMessage);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new PlatformException(Messages.PasswordContentMessage);
            }

            try
            {
                // Get control data bytes
                var rawControlData = Convert.FromBase64String(controlContent);
                var symmetricKey = SymmetricKey.FromToken(rawControlData);

                if (!password.Equals(Encoding.UTF8.GetString(symmetricKey.Password)))
                {
                    throw new PlatformException(Messages.InvalidControlContentMessage);
                }

                // Get bytes from content
                var rawContent = Convert.FromBase64String(content);

                // Decrypt the content
                using var outputStream = new MemoryStream();
                using (var aes = SymmetricCryptography.CreateAesProvider(symmetricKey))
                {
                    using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using var inputStream = new MemoryStream(rawContent);
                    using var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read);
                    cryptoStream.CopyTo(outputStream);
                }

                // Returns the decrypted content
                var decryptedContent = outputStream.ToArray();
                return Encoding.UTF8.GetString(decryptedContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(Messages.UnprotectExceptionMessage, e);
            }
        }
    }
}