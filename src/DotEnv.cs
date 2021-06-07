using System;
using System.IO;

namespace Vreval.Platform
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Could not find .env-file in " + Directory.GetCurrentDirectory());
                return;
            }
            Console.WriteLine(".env-file found in " + Directory.GetCurrentDirectory());
                

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=');

                if (parts.Length != 2)
                    continue;
                
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}