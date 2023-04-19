using System.Text;

namespace Aurora.Framework.Cryptography
{
    internal class SymmetricKey
    {
        private const char cTokenSeparator = '$';

        internal byte[] Key { get; set; }
        internal byte[] IV { get; set; }
        internal byte[] Password { get; set; }

        private SymmetricKey() { }

        internal SymmetricKey(string password)
        {
            using var aes = SymmetricCryptography.CreateAesProvider();
            aes.GenerateKey();
            aes.GenerateIV();

            Key = aes.Key;
            IV = aes.IV;
            Password = Encoding.UTF8.GetBytes(password);
        }

        internal static SymmetricKey FromToken(byte[] token)
        {
            var tokens = Encoding
                .UTF8
                .GetString(token)
                .Split(cTokenSeparator);

            return new SymmetricKey
            {
                Key = Convert.FromBase64String(tokens[0]),
                IV = Convert.FromBase64String(tokens[1]),
                Password = Convert.FromBase64String(tokens[2])
            };
        }

        internal byte[] ToToken()
        {
            var tokens = string.Concat(
                Convert.ToBase64String(Key),
                cTokenSeparator,
                Convert.ToBase64String(IV),
                cTokenSeparator,
                Convert.ToBase64String(Password));

            return Encoding
                .UTF8
                .GetBytes(tokens);
        }
    }
}