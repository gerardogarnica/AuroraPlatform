using System.Security.Cryptography;

namespace Aurora.Framework.Cryptography
{
    internal static class SymmetricCryptography
    {
        private const int keySize = 256;

        internal static Aes CreateAesProvider()
        {
            using var aes = Aes.Create();
            aes.KeySize = keySize;
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            return aes;
        }

        internal static Aes CreateAesProvider(SymmetricKey symmetricKey)
        {
            var aes = CreateAesProvider();

            if (symmetricKey == null)
            {
                aes.GenerateKey();
                aes.GenerateIV();
            }
            else
            {
                aes.Key = symmetricKey.Key;
                aes.IV = symmetricKey.IV;
            }

            return aes;
        }
    }
}