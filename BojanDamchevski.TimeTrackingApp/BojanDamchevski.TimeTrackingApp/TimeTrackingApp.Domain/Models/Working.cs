using System;
using System.Diagnostics;
using TimeTrackingApp.Domain.Enums;

namespace TimeTrackingApp.Domain.Models
{
    public class Working
    {
        public string Title { get; set; }
        public ActivityType Type { get; set; }
        public WorkingType WorkingType { get; set; }

        public Working()
        {
            Title = "Working Activity";
            Type = ActivityType.Working;
        }
        public void WorkingStopwatch(WorkingType workingType, User user)
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
                if (workingType == WorkingType.Home)
                {
                    user.Home += convertedTime / 60;
                }
                if (workingType == WorkingType.Office)
                {
                    user.Office += convertedTime / 60;
                }
                user.TimeWorking += convertedTime / 60;
                Console.WriteLine($"Time spent on this activity [{Title}] (For total time go to statistics in the main menu): {Math.Round(convertedTime / 60, 2)} minutes.");
                Console.WriteLine("Press any key to go back to the Main Menu.");
                Console.ReadKey();
            }
        }
        public void WorkingActivity(User user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose working space:\n");
                Console.WriteLine("1.) Home");
                Console.WriteLine("2.) Office");
                bool choiceValidation = int.TryParse(Console.ReadLine(), out int choice);
                if (choiceValidation)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("Working from home...");
                        WorkingStopwatch(WorkingType.Home, user);
                        flag = false;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Working from the office...");
                        WorkingStopwatch(WorkingType.Office, user);
                        flag = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error. Try again.");
                        Console.WriteLine("Press any key to continue.");
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
