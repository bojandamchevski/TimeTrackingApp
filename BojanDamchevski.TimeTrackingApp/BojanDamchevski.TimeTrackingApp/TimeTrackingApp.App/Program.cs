using System;
using TimeTrackingApp.Domain.Database;
using TimeTrackingApp.Domain.Models;
using TimeTrackingApp.Services.Implementations;
using TimeTrackingApp.Services.Interfaces;

namespace TimeTrackingApp.App
{
    class Program
    {
        public static void LandingPage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("             Time Tracking App\nThe most valuable resources on the planet\n\n\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please choose one of the following options:\n");
            Console.WriteLine("1.) Log in                                2.) Register                              3.) Exit");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   (Press 1 to Log in to your account)       (Press 2 to create a new account)          (Press 3 to exit)");
            bool flag = true;
            while (flag)
            {
                bool validationLandingPageChoice = int.TryParse(Console.ReadLine(), out int landingPageChoice);
                if (validationLandingPageChoice)
                {
                    if (landingPageChoice == 1)
                    {
                        if (_userService.LogIn() < 1)
                        {
                            LandingPage();
                        }
                        else
                        {
                            LandingPage();
                            flag = false;
                        }
                    }
                    else if (landingPageChoice == 2)
                    {
                        _userService.Register();
                        Console.ReadKey();
                        LandingPage();
                        flag = false;
                    }
                    else if (landingPageChoice == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error bad input !\nTry Again.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error bad input !\nTry Again.");
                }
            }
        }
        public static IUserService<User> _userService = new UserService<User>();
        static void Main(string[] args)
        {
            //_userService.AddUser(new User("Bojan", "Damchevski", 23, "Bojan123", "Bojan111"));
            //_userService.AddUser(new User("Stefan", "Trajkov", 22, "Stefan123", "Stefan111"));
            //_userService.AddUser(new User("Jovana", "Miskimovska", 24, "Jovana123", "Jovana111"));
            //_userService.AddUser(new User("Nenad", "Poposki", 33, "Nenad123", "Nenad111"));
            //_userService.AddUser(new User("Filip", "Trajanovski", 25, "Filip123", "Filip111"));
            //_userService.AddUser(new User("Aleksandar", "Manasievikj", 29, "Aleksandar123", "Aleksandar111"));

            LandingPage();
        }
    }
}
