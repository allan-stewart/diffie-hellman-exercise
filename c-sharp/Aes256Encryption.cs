using System.IO;
using System.Security.Cryptography;

namespace DiffieHellman
{
    class Aes256Encryption
    {
        public byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using (var algorithm = new AesManaged())
            {
                algorithm.Key = key;
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.CBC;

                var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (var algorithm = new AesManaged())
            {
                algorithm.Key = key;
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.CBC;

                var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);
                using (var memoryStream = new MemoryStream(cipherText))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
