using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTrackingApp.Domain.Models;

namespace TimeTrackingApp.Services.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateUsername(User user)
        {

            if (user.Username.Length < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The username can not be less than 5 characters.");
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Username approved !");
                return true;
            }
        }
        public static bool ValidatePassword(User user)
        {
            if (user.Password.Length < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The password can not be less than 6 characters.");
                return false;
            }
            else
            {
                if (user.Password.Any(char.IsUpper) && user.Password.Any(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Password approved.");
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password must contain at least one capital letter and at least one number.");
                    return false;
                }
            }
        }
        public static bool ValidateFirstNameLastName(User user)
        {
            if (user.FirstName.Length < 2 && user.LastName.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("First and last name can not be shorter than 2 characters.");
                return false;
            }
            else
            {
                bool firstNameContainsNumber = user.FirstName.Any(char.IsDigit);
                bool lastNameContainsNumber = user.LastName.Any(char.IsDigit);
                bool firstNameContainsSymbol = user.FirstName.Any(char.IsSymbol);
                bool lastNameContainsSymbol = user.LastName.Any(char.IsSymbol);
                if (firstNameContainsNumber || lastNameContainsNumber || lastNameContainsSymbol || firstNameContainsSymbol)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("First and last name must not contain numbers or symbols.");
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("First and last name approved !");
                    return true;
                }
            }
        }
        public static bool ValidateAge(User user)
        {
            {
                if (user.Age < 18 || user.Age > 120)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Age must be from 18 to 120.");
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Age approved !");
                    return true;
                }

            }

        }
    }
}
