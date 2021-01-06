using System;
using System.IO;

namespace DiffieHellman
{
    class SimpleEncryption
    {
        public void Encrypt(string simpleKey, string plaintextFile, string ciphertextFile)
        {
            Console.WriteLine($"Simple key: {simpleKey}");
            Console.WriteLine($"Plaintext file: {plaintextFile}");

            var key = CreateKeyFromSimpleKey(simpleKey);
            var iv = new RandomByteGenerator().Generate(16);
            var plaintext = File.ReadAllText(plaintextFile);
            var ciphertext = new Aes256Encryption().Encrypt(plaintext, key, iv);

            File.WriteAllText(ciphertextFile, $"{ToBase64(iv)}\n{ToBase64(ciphertext)}");
            Console.WriteLine($"Ciphtertext written to: {ciphertextFile}");
        }

        public void Decrypt(string simpleKey, string ciphertextFile, string plaintextFile)
        {
            Console.WriteLine($"Simple key: {simpleKey}");
            Console.WriteLine($"Ciphertext file: {ciphertextFile}");

            var data = File.ReadAllText(ciphertextFile).Split("\n");
            var key = CreateKeyFromSimpleKey(simpleKey);
            var iv = FromBase64(data[0]);
            var ciphertext = FromBase64(data[1]);
            var plaintext = new Aes256Encryption().Decrypt(ciphertext, key, iv);
            
            File.WriteAllText(plaintextFile, plaintext);
            Console.WriteLine($"Plaintext written to: {plaintextFile}");
        }

        private byte[] CreateKeyFromSimpleKey(string key)
        {
            return new Sha256Hasher().Hash(key);
        }

        private string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private byte[] FromBase64(string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}
