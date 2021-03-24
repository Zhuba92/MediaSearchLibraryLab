using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            Console.Write("\nWould you like to search for a movie (Y/N): ");
            string decision = Console.ReadLine().ToUpper();

            while(decision == "Y")
            {
                string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
                MovieFile movieFile = new MovieFile(scrubbedFile);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("\nWhat would you like to search for in the movie titles: ");
                string searchedTitle = Console.ReadLine();
                var searchedMovie = movieFile.Movies.Where(m => m.title.Contains(searchedTitle));
                Console.WriteLine($"\n**There are {searchedMovie.Count()} movies that contain {searchedTitle} in the title**\n");
                foreach(Movie movie in searchedMovie)
                {
                    Console.WriteLine(movie.title);
                }
                Console.Write("\nWould you like to search for another movie? (Y/N): ");
                decision = Console.ReadLine().ToUpper();
            }

            // // LINQ - Where filter operator & Contains quantifier operator
            // var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            // // LINQ - Count aggregation method
            // Console.WriteLine($"There are {Movies.Count()} movies from 1990");

            // // LINQ - Any quantifier operator & Contains quantifier operator
            // var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
            // Console.WriteLine($"Any movies from 1921? {validate}");

            // // LINQ - Where filter operator & Contains quantifier operator & Count aggregation method
            // int num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
            // Console.WriteLine($"There are {num} movies from 1921");

            // // LINQ - Where filter operator & Contains quantifier operator
            // var Movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
            // foreach(Movie m in Movies1921)
            // {
            //     Console.WriteLine($"  {m.title}");
            // }

            // // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
            // var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
            // // LINQ - Count aggregation method
            // Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
            // foreach(string t in titles)
            // {
            //     Console.WriteLine($"  {t}");
            // }

            // // LINQ - First element operator
            // var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
            // Console.WriteLine($"First movie that starts with letter 'Z': {FirstMovie.title}");

            // Console.ForegroundColor = ConsoleColor.White;

            logger.Info("Program ended");
        }
    }
}