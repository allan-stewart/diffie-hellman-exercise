using System;
using System.IO;
using System.Linq;

namespace DiffieHellman
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            var command = args[0];
            var validCommands = new [] { "key-exchange", "encrypt", "decrypt" };
            if (!validCommands.Any(x => x == args[0])) {
                Console.WriteLine($"Unknown command: {command}\n");
                PrintUsage();
                return;
            }

            if (command == "key-exchange")
            {
                new KeyExchange().Exchange();
            }
            else
            {
                EncryptDecrypt(args);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("dotnet run key-exchange");
            Console.WriteLine("dotnet run <encrypt|decrypt> <simpleKey> <inputFile> <outputFile>");
        }

        static void EncryptDecrypt(string[] args)
        {
            if (args.Length < 4) {
                PrintUsage();
                return;
            }
            
            var command = args[0];
            var simpleKey = args[1];
            var inputFile = args[2];
            var outputFile = args[3];
            
            if (!File.Exists(inputFile)) {
                Console.WriteLine($"File does not exist: {inputFile}\n");
                PrintUsage();
                return;
            }

            var simple = new SimpleEncryption();
            if (command == "encrypt")
            {
                simple.Encrypt(simpleKey, inputFile, outputFile);
            }
            else
            {
                simple.Decrypt(simpleKey, inputFile, outputFile);
            }
        }
    }
}
