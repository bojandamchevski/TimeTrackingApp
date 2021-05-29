using System;
using System.Diagnostics;
using TimeTrackingApp.Domain.Enums;

namespace TimeTrackingApp.Domain.Models
{
    public class Reading
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public ActivityType Type { get; set; }
        public ReadingType ReadingType { get; set; }
        public Reading()
        {
            Title = "Reading Activity";
            Pages = 0;
            Type = ActivityType.Reading;
        }
        public void ReadingStopwatch(ReadingType readingType, User user)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press ENTER to stop.");
            string stop = Console.ReadLine();
            if (stop == "")
            {
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                double convertedTime = Convert.ToDouble(time.TotalSeconds);
                if (readingType == ReadingType.BellesLetres)
                {
                    user.TotalPages += Pages;
                    user.BellesLettres += convertedTime / 60;
                }
                if (readingType == ReadingType.Fiction)
                {
                    user.TotalPages += Pages;
                    user.Fiction += convertedTime / 60;
                }
                if (readingType == ReadingType.ProfessionalLiterature)
                {
                    user.TotalPages += Pages;
                    user.ProfessionalLiterature += convertedTime / 60;
                }
                user.TimeReading += convertedTime / 60;
                Console.WriteLine($"Time spent on this activity [{Title}] (For total time go to statistics in the main menu): {Math.Round(convertedTime / 60, 2)} minutes.");
                Console.WriteLine("Press any key to go back to the Main Menu.");
                Console.ReadKey();
            }
        }
        public void ReadingActivity(User user)
        {
            bool readingflag = true;
            while (readingflag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose what to read:\n");
                Console.WriteLine("1.) Belles Lettres");
                Console.WriteLine("2.) Fiction");
                Console.WriteLine("3.) Professional Literature");
                bool choiceValidation = int.TryParse(Console.ReadLine(), out int choice);
                if (choiceValidation)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("Reading Belles Lettres");
                        Pages += 10;
                        ReadingStopwatch(ReadingType.BellesLetres, user);
                        readingflag = false;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Reading Fiction");
                        Pages += 12;
                        ReadingStopwatch(ReadingType.Fiction, user);
                        readingflag = false;
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Reading Professional Literature");
                        Pages += 15;
                        ReadingStopwatch(ReadingType.ProfessionalLiterature, user);
                        readingflag = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error. Try again.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad input. Try again.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
    }
}
