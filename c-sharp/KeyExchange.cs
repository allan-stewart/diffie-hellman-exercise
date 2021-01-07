using System;
using System.Numerics;

namespace DiffieHellman
{
    class KeyExchange
    {
        public void Exchange()
        {
            Console.Write("Enter a secret: ");
            BigInteger p = 97;
            BigInteger g = 18;
            var a = BigInteger.Parse(Console.ReadLine().Trim());
            var A = BigInteger.ModPow(g, a, p);
            Console.WriteLine($"Value to share: {A}");

            Console.Write("Enter the public value from the other person: ");
            var B = BigInteger.Parse(Console.ReadLine().Trim());
            var simpleKey = BigInteger.ModPow(B, a, p);
            Console.WriteLine($"Simple key: {simpleKey}");
        }
    }
}
