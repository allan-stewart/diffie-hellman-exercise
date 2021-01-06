using System.Security.Cryptography;

namespace DiffieHellman
{
    public class RandomByteGenerator
    {
        private static readonly RNGCryptoServiceProvider byteGenerator = new RNGCryptoServiceProvider();

        public byte[] Generate(int numberOfBytes)
        {
            var bytes = new byte[numberOfBytes];
            byteGenerator.GetBytes(bytes);
            return bytes;
        }
    }
}
