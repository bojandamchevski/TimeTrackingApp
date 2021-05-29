using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AccountActivity { get; set; }
        public double TimeReading { get; set; }
        public double BellesLettres { get; set; }
        public double Fiction { get; set; }
        public double ProfessionalLiterature { get; set; }
        public double TimeExercising { get; set; }
        public double General { get; set; }
        public double Running { get; set; }
        public double Sport { get; set; }
        public string FavouriteExerciseType { get; set; }
        public double TimeWorking { get; set; }
        public double Home { get; set; }
        public double Office { get; set; }
        public double TimeOtherHobbies { get; set; }
        public int TotalPages { get; set; }
        public string FavouriteActivity { get; set; }
        public string FavouriteReadingTypeActivity { get; set; }
        public string FavouriteExercisingTypeActivity { get; set; }
        public string FavouriteWorkingTypeActivity { get; set; }
        public List<string> Hobbies { get; set; }
        public User(string firstName, string lastName, int age, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
            TimeReading = 0;
            TotalPages = 0;
            TimeExercising = 0;
            TimeWorking = 0;
            TimeOtherHobbies = 0;
            FavouriteActivity = "";
            AccountActivity = true;
            Hobbies = new List<string>();
        }
        public override void GetInfo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login successful !\n");
            Console.WriteLine($"Welcome {FirstName} {LastName}\n");
        }
    }
}
