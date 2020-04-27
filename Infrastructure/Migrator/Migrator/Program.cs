using System;
using System.IO;
using Npgsql;

namespace Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Directory path not passed. Exit from program");
                Environment.Exit(1);
            }
            
            var directory = args[0];
            Console.WriteLine($"Execute files from directory:{directory}");
            
            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Directory doesn't exist. Exit from program");
                Environment.Exit(1);
            }

            var files = Directory.GetFiles(directory, "*.sql", SearchOption.AllDirectories);
            Console.WriteLine($"Found {files.Length} files in directory");

            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var db = Environment.GetEnvironmentVariable("DB_DATABASE");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                foreach (var file in files)
                {
                    Console.WriteLine($"Proceed file:{file}");
                    try
                    {
                        var commandText = File.ReadAllText(file);
                        using (var command = new NpgsqlCommand(commandText, connection))
                            command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error on process files: {file}: {e.Message}", ConsoleColor.Red);
                        Environment.Exit(1);
                    }
                }
            }
            Console.WriteLine("All files processed. Migration done!", ConsoleColor.Green);
        }
    }
}