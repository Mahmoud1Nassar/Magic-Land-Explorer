using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Magic_Land_Explorer
{
    class Program
    {
        delegate void MenuAction(List<Category> categories);

        static void Main(string[] args)
        {
            string json = File.ReadAllText("./data.json");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);

            // Interactive Console Interface
            var menuActions = new Dictionary<string, MenuAction>
            {
                { "1", ShowFilteredDestinations },
                { "2", ShowLongestDuration },
                { "3", SortDestinationsByName },
                { "4", ShowTop3LongestDuration }
            };

            while (true)
            {
                Console.WriteLine("1- Show Filtered Destinations");
                Console.WriteLine("2- Show Longest Duration");
                Console.WriteLine("3- Sort Destinations by Name");
                Console.WriteLine("4- Show Top 3 Longest Duration");
                Console.WriteLine("5- Exit");

                var input = Console.ReadLine();
                if (input == "5") break;

                if (menuActions.ContainsKey(input))
                {
                    menuActions[input](categories);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        // LINQ Query Tasks
        static void ShowFilteredDestinations(List<Category> categories)
        {
            var filteredDestinations = categories
                .SelectMany(c => c.Destinations)
                .Where(d => TryParseDuration(d.Duration, out int duration) && duration < 100)
                .Select(d => d.Name);

            Console.WriteLine("Filtered Destinations with duration less than 100 minutes:");
            foreach (var destination in filteredDestinations)
            {
                Console.WriteLine(destination);
            }
        }

        static void ShowLongestDuration(List<Category> categories)
        {
            var longestDurationDestination = categories
                .SelectMany(c => c.Destinations)
                .Where(d => TryParseDuration(d.Duration, out _))
                .OrderByDescending(d => int.Parse(d.Duration.Replace(" minutes", "")))
                .FirstOrDefault();

            if (longestDurationDestination != null)
            {
                Console.WriteLine("Destination with the longest duration:");
                Console.WriteLine($"{longestDurationDestination.Name} - {longestDurationDestination.Duration}");
            }
        }

        static void SortDestinationsByName(List<Category> categories)
        {
            var sortedDestinations = categories
                .SelectMany(c => c.Destinations)
                .OrderBy(d => d.Name)
                .Select(d => d.Name);

            Console.WriteLine("Destinations sorted by name:");
            foreach (var destination in sortedDestinations)
            {
                Console.WriteLine(destination);
            }
        }

        static void ShowTop3LongestDuration(List<Category> categories)
        {
            var top3LongestDuration = categories
                .SelectMany(c => c.Destinations)
                .Where(d => TryParseDuration(d.Duration, out _))
                .OrderByDescending(d => int.Parse(d.Duration.Replace(" minutes", "")))
                .Take(3);

            Console.WriteLine("Top 3 longest duration destinations:");
            foreach (var destination in top3LongestDuration)
            {
                Console.WriteLine($"{destination.Name} - {destination.Duration}");
            }
        }

        static bool TryParseDuration(string durationString, out int duration)
        {
            duration = 0;
            if (string.IsNullOrWhiteSpace(durationString))
            {
                return false;
            }

            durationString = durationString.Replace(" minutes", "");
            return int.TryParse(durationString, out duration);
        }
    }
}
