// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                
            }

            Console.WriteLine("Enter file path:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }

            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");

                // Read the file content
                string content = File.ReadAllText(filePath);

                // Count lines
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");

                // 1. Count words
                string[] words = Regex.Split(content.ToLower(), @"\W+")
                    .Where(w => !string.IsNullOrWhiteSpace(w))
                    .ToArray();
                int wordCount = words.Length;
                Console.WriteLine($"Number of words: {wordCount}");

                // 2. Count characters (with and without whitespace)
                int charCountWithSpaces = content.Length;
                int charCountWithoutSpaces = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"Number of characters (including spaces): {charCountWithSpaces}");
                Console.WriteLine($"Number of characters (excluding spaces): {charCountWithoutSpaces}");

                // 3. Count sentences
                int sentenceCount = Regex.Matches(content, @"[.!?]+(\s|$)")
                    .Count;
                Console.WriteLine($"Number of sentences: {sentenceCount}");

                // 4. Identify most common words
                var mostCommon = words
                    .GroupBy(w => w)
                    .OrderByDescending(g => g.Count())
                    .First();

                Console.WriteLine($"\nMost common word: '{mostCommon.Key}' appears {mostCommon.Count()} times");

                // 5. Average word length
                double avgWordLength = words.Any() ? words.Average(w => w.Length) : 0;
                Console.WriteLine($"\nAverage word length: {avgWordLength:F2} characters");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}