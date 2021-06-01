using TimeTrackingApp.Domain.Models;
using TimeTrackingApp.Services.Interfaces;
using TimeTrackingApp.Domain.Database;
using System;
using TimeTrackingApp.Services.Helpers;
using System.Diagnostics;

namespace TimeTrackingApp.Services.Implementations
{
    public class UserService<T> : IUserService<T> where T : User
    {
        public Reading readingActivity;
        public Exercising exercisingActivity;
        public Working workingActivity;
        public OtherHobbies otherHobbiesActivity;
        private IDatabase<T> _database;
        public UserService()
        {
            _database = new FileDatabase<T>();
            readingActivity = new Reading();
            exercisingActivity = new Exercising();
            workingActivity = new Working();
            otherHobbiesActivity = new OtherHobbies();
        }

        public void AddUser(T user)
        {
            _database.InsertUser(user);
        }

        public T ChangeFristName(T user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter new first name:");
                string newFirstName = Console.ReadLine();
                user.FirstName = newFirstName;
                if (ValidationHelper.ValidateFirstNameLastName(user) == true)
                {
                    flag = false;
                    Console.WriteLine("Success!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return user;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
            return (T)user;
        }
        public T ChangeLastName(T user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter new last name:");
                string newLastName = Console.ReadLine();
                user.LastName = newLastName;
                if (ValidationHelper.ValidateFirstNameLastName(user) == true)
                {
                    flag = false;
                    Console.WriteLine("Success!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return user;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
            return (T)user;
        }

        public T ChangePassword(T user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter new password:");
                string newPassword = Console.ReadLine();
                user.Password = newPassword;
                if (ValidationHelper.ValidatePassword(user) == true)
                {
                    flag = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Success!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return user;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again, password is not strong !");
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
            return (T)user;
        }

        public T DeactivateAccount(T userInput)
        {
            User user = null;
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                user = _database.RemoveUser(userInput);
                if (user == null)
                {
                    Console.WriteLine("User with that information does not exits");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Success!");
                    Console.WriteLine("User account deactivated");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    flag = false;
                    return (T)user;
                }
            }
            return (T)user;
        }

        public int LogIn()
        {
            User user = null;
            int i = 4;
            while (i > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter log in information\n");
                Console.WriteLine("Enter username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                user = _database.CheckUser(username, password);
                if (user != null)
                {
                    bool menuflag = true;
                    while (menuflag)
                    {
                        if (user.AccountActivity == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("This account has been deactivated.\nDo you want to activate it again ?\nType Yes to activate or press ENTER to go back to the log in with another account.");
                            string response = Console.ReadLine();
                            if (response == "Yes")
                            {
                                _database.ActivateAccount(user);
                            }
                            if(response == "")
                            {
                                break;
                            }
                        }
                        Console.Clear();
                        user.GetInfo();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Welcome to the main menu !");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Choose one of the following options:");
                        Console.WriteLine("1.) Track");
                        Console.WriteLine("2.) Statistics");
                        Console.WriteLine("3.) Account Management");
                        Console.WriteLine("4.) Log out");
                        bool menuChoiceValidation = int.TryParse(Console.ReadLine(), out int menuChoice);
                        if (menuChoiceValidation)
                        {
                            if (menuChoice == 1)
                            {
                                Track((T)user);
                                menuflag = true;
                            }
                            else if (menuChoice == 2)
                            {
                                Statistics((T)user);
                                menuflag = true;
                            }
                            else if (menuChoice == 3)
                            {
                                bool accManageFlag = true;
                                while (accManageFlag)
                                {
                                    Console.Clear();
                                    Console.WriteLine("1.) Deactivate account");
                                    Console.WriteLine("2.) Change password");
                                    Console.WriteLine("3.) Change first name");
                                    Console.WriteLine("4.) Change last name");
                                    Console.WriteLine("5.) Back to the main menu");
                                    bool accountManagementValidation = int.TryParse(Console.ReadLine(), out int accManagementChoice);
                                    if (accountManagementValidation)
                                    {

                                        if (accManagementChoice == 1)
                                        {
                                            DeactivateAccount((T)user);
                                            menuflag = false;
                                            return 1;
                                        }
                                        else if (accManagementChoice == 2)
                                        {
                                            _database.UpdateUser(ChangePassword((T)user));
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            menuflag = false;
                                            return 2;
                                        }
                                        else if (accManagementChoice == 3)
                                        {
                                            _database.UpdateUser(ChangeFristName((T)user));
                                            menuflag = false;
                                            return 3;
                                        }
                                        else if (accManagementChoice == 4)
                                        {
                                            _database.UpdateUser(ChangeLastName((T)user));
                                            menuflag = false;
                                            return 3;
                                        }
                                        else if (accManagementChoice == 5)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Bad input please try again !");
                                            Console.WriteLine("Press any key to continue.");
                                            Console.ReadKey();
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Bad input please try again !");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else if (menuChoice == 4)
                            {
                                LogOut();
                                menuflag = false;
                                return 9;
                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Bad input please try again !");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Bad input please try again !");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect username or password !");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Press any key to enter login information again !\nYou have [{i - 1}] attempts left.");
                    if (i == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("No more attempts left. Goodbye");
                    }
                    Console.ReadKey();
                }
                i--;
            }
            return 0;
        }

        public int LogOut()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Logging out...");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return 9;
        }

        public void Register()
        {
            bool flag = true;
            while (flag)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Create a new account.\nType information bellow\n");
                Console.WriteLine("Enter first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter last name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter age:");
                bool ageValidatorSuccess = int.TryParse(Console.ReadLine(), out int age);
                User newUser = new User(firstName, lastName, age, username, password);
                if (ageValidatorSuccess && ValidationHelper.ValidateUsername(newUser) && ValidationHelper.ValidatePassword(newUser) && ValidationHelper.ValidateFirstNameLastName(newUser) && ValidationHelper.ValidateAge(newUser))
                {
                    _database.InsertUser((T)newUser);
                    Console.WriteLine("Success!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    flag = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error bad input !\nTry again.");
                    Console.WriteLine(ValidationHelper.ValidateUsername(newUser));
                    Console.WriteLine(ValidationHelper.ValidatePassword(newUser));
                    Console.WriteLine(ValidationHelper.ValidateFirstNameLastName(newUser));
                    Console.WriteLine(ValidationHelper.ValidateAge(newUser));
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
        public void Statistics(T user)
        {
            bool statisticsFlag = true;
            while (statisticsFlag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Choose one of the following statistics menu options:");
                Console.WriteLine("1.) Reading statistics");
                Console.WriteLine("2.) Exercising statistics");
                Console.WriteLine("3.) Working statistics");
                Console.WriteLine("4.) Hobby statistics");
                Console.WriteLine("5.) Global statistics");
                Console.WriteLine("6.) Back to main menu");
                bool statisticsValidation = int.TryParse(Console.ReadLine(), out int statisticsChoice);
                if (statisticsValidation)
                {
                    if (statisticsChoice == 1)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome to statistics for {user.FirstName} {user.LastName}\n");
                        Console.WriteLine("Reading statistics:\n\n");
                        Console.WriteLine($"Time reading: {Math.Round(user.TimeReading / 60, 4)} hours.\n");
                        Console.WriteLine($"Time reading: {Math.Round(user.TimeReading, 2)} minutes.\n");
                        Console.WriteLine($"Time reading [Belles Lettres]: {Math.Round(user.BellesLettres, 2)} minutes.\n");
                        Console.WriteLine($"Time reading [Fiction]: {Math.Round(user.Fiction, 2)} minutes.\n");
                        Console.WriteLine($"Time reading [Professional Literature]: {Math.Round(user.ProfessionalLiterature, 2)} minutes.\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (user.BellesLettres > user.Fiction && user.BellesLettres > user.ProfessionalLiterature)
                        {
                            user.FavouriteReadingTypeActivity = "Belles Lettres";
                        }
                        else if (user.Fiction > user.BellesLettres && user.Fiction > user.ProfessionalLiterature)
                        {
                            user.FavouriteReadingTypeActivity = "Fiction";
                        }
                        else
                        {
                            user.FavouriteReadingTypeActivity = "Professional Literature";
                        }
                        Console.WriteLine($"Favourite reading type:\n{user.FavouriteReadingTypeActivity}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Total pages: {user.TotalPages}\n");
                        statisticsFlag = false;
                        Console.WriteLine("Press any key to go to the Main Menu.");
                        Console.ReadKey();
                    }
                    else if (statisticsChoice == 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome to statistics for {user.FirstName} {user.LastName}\n");
                        Console.WriteLine("Exercising statistics:\n\n");
                        Console.WriteLine($"Time exercising: {Math.Round(user.TimeExercising / 60, 4)} hours.\n");
                        Console.WriteLine($"Time exercising: {Math.Round(user.TimeExercising, 2)} minutes.\n");
                        Console.WriteLine($"Time exercising [general]: {Math.Round(user.General, 2)} minutes.\n");
                        Console.WriteLine($"Time exercising [running]: {Math.Round(user.Running, 2)} minutes.\n");
                        Console.WriteLine($"Time exercising [sport]: {Math.Round(user.Sport, 2)} minutes.\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (user.General > user.Running && user.General > user.Sport)
                        {
                            user.FavouriteExerciseType = "General";
                        }
                        else if (user.Running > user.General && user.Running > user.Sport)
                        {
                            user.FavouriteExerciseType = "Running";
                        }
                        else
                        {
                            user.FavouriteExerciseType = "Sport";
                        }
                        Console.WriteLine($"Favourite exercising type:\n{user.FavouriteExerciseType}");
                        statisticsFlag = false;
                        Console.WriteLine("Press any key to go to the Main Menu.");
                        Console.ReadKey();
                    }
                    else if (statisticsChoice == 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome to statistics for {user.FirstName} {user.LastName}\n");
                        Console.WriteLine("Working statistics:\n\n");
                        Console.WriteLine($"Time working: {Math.Round(user.TimeWorking / 60, 2)} hours.\n");
                        Console.WriteLine($"Time working: {Math.Round(user.TimeWorking, 2)} minutes.\n");
                        Console.WriteLine($"Time working from home: {Math.Round(user.Home / 60, 2)} hours.\n");
                        Console.WriteLine($"Time working from the office: {Math.Round(user.Office / 60, 2)} hours.\n");
                        statisticsFlag = false;
                        Console.WriteLine("Press any key to go to the Main Menu.");
                        Console.ReadKey();
                    }
                    else if (statisticsChoice == 4)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome to statistics for {user.FirstName} {user.LastName}\n");
                        Console.WriteLine($"Time doing other hobbies: {Math.Round(user.TimeOtherHobbies, 2)} minutes.\n");
                        Console.WriteLine($"List of your hobbies:");
                        foreach (string item in user.Hobbies)
                        {
                            Console.WriteLine(item);
                        }
                        statisticsFlag = false;
                        Console.WriteLine("Press any key to go to the Main Menu.");
                        Console.ReadKey();

                    }
                    else if (statisticsChoice == 5)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome to statistics for {user.FirstName} {user.LastName}\n");
                        if (user.TimeReading > user.TimeExercising && user.TimeReading > user.TimeWorking && user.TimeReading > user.TimeOtherHobbies)
                        {
                            user.FavouriteActivity = "Your favourite activity is Reading !\n";
                        }
                        else if (user.TimeExercising > user.TimeReading && user.TimeExercising > user.TimeWorking && user.TimeExercising > user.TimeOtherHobbies)
                        {
                            user.FavouriteActivity = "Your favourite activity is Exercising !\n";
                        }
                        else if (user.TimeWorking > user.TimeReading && user.TimeWorking > user.TimeExercising && user.TimeWorking > user.TimeOtherHobbies)
                        {
                            user.FavouriteActivity = "Your favourite activity is Working !\n";
                        }
                        else
                        {
                            user.FavouriteActivity = "Your favourite activity is doing other hobbies !\n";
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Favourite activity: {user.FavouriteActivity}");
                        double totalTime = user.TimeReading + user.TimeWorking + user.TimeExercising + user.TimeOtherHobbies;
                        Console.WriteLine($"Total time spent on all activities: {Math.Round(totalTime / 60, 3)} hours");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Press any key to go to the Main Menu.");
                        Console.ReadKey();
                    }
                    else if (statisticsChoice == 6)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bad input try again !");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad input try again !");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        public void Track(T user)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Choose activity:\n");
                Console.WriteLine("1.) Reading");
                Console.WriteLine("2.) Exercising");
                Console.WriteLine("3.) Working");
                Console.WriteLine("4.) Other hobbies");
                Console.WriteLine("5.) Back to main menu");
                bool activityValidation = int.TryParse(Console.ReadLine(), out int activityChoice);
                if (activityValidation)
                {
                    if (activityChoice <= 5 && activityChoice > 0)
                    {

                        if (activityChoice == 1)
                        {
                            readingActivity.ReadingActivity(user);
                            _database.UpdateUser(user);
                            flag = false;
                        }
                        else if (activityChoice == 2)
                        {
                            exercisingActivity.ExercisingActivity(user);
                            _database.UpdateUser(user);
                            flag = false;
                        }
                        else if (activityChoice == 3)
                        {
                            workingActivity.WorkingActivity(user);
                            _database.UpdateUser(user);
                            flag = false;
                        }
                        else if (activityChoice == 4)
                        {
                            otherHobbiesActivity.OtherHobiesActivity(user);
                            _database.UpdateUser(user);
                            flag = false;
                        }
                        else if (activityChoice == 5)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please try again !");
                        Console.WriteLine("Press any key to try again.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again !");
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }
    }
}
