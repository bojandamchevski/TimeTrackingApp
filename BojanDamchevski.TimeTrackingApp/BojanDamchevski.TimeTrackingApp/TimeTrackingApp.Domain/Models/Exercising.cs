using System;
using System.Diagnostics;
using TimeTrackingApp.Domain.Enums;

namespace TimeTrackingApp.Domain.Models
{
    public class Exercising
    {
        public string Title { get; set; }
        public ActivityType Type { get; set; }
        public ExercisingType ExercisingType { get; set; }

        public Exercising()
        {
            Title = "Exercising Activity";
            Type = ActivityType.Exercising;
        }
        public void ExercisingStopwatch(ExercisingType exercisingType, User user)
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
                if (exercisingType == ExercisingType.General)
                {
                    user.General += convertedTime / 60;
                }
                if (exercisingType == ExercisingType.Running)
                {
                    user.Running += convertedTime / 60;
                }
                if (exercisingType == ExercisingType.Sport)
                {
                    user.Sport += convertedTime / 60;
                }
                user.TimeExercising += convertedTime / 60;
                Console.WriteLine($"Time spent on this activity [{Title}] (For total time go to statistics in the main menu): {Math.Round(convertedTime / 60, 2)} minutes.");
                Console.WriteLine("Press any key to go back to the Main Menu.");
                Console.ReadKey();
            }
        }
        public void ExercisingActivity(User user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose exercise:\n");
                Console.WriteLine("1.) General");
                Console.WriteLine("2.) Running");
                Console.WriteLine("3.) Sport");
                bool choiceValidation = int.TryParse(Console.ReadLine(), out int choice);
                if (choiceValidation)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("General...");
                        ExercisingStopwatch(ExercisingType.General, user);
                        flag = false;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Running...");
                        ExercisingStopwatch(ExercisingType.Running, user);
                        flag = false;
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Sport...");
                        ExercisingStopwatch(ExercisingType.Sport, user);
                        flag = false;
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
