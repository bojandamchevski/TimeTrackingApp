using TimeTrackingApp.Domain.Models;

namespace TimeTrackingApp.Domain.Database
{
    public interface IDatabase<T> where T : User
    {
        void InsertUser(T user);
        User RemoveUser(User removedUser);
        User ActivateAccount(User activatedUser);
        void UpdateUser(T user);
        User CheckUser(string username, string password);

    }
}
