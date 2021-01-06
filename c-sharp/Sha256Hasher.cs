using System.Security.Cryptography;
using System.Text;

namespace DiffieHellman
{
    class Sha256Hasher
    {
        public byte[] Hash(string input)
        {
            using (var hasher = SHA256.Create())
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(input ?? ""));
            }
        }
    }
}
