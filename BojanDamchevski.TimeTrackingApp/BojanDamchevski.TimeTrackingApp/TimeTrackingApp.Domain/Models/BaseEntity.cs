using TimeTrackingApp.Domain.Interfaces;

namespace TimeTrackingApp.Domain.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public abstract void GetInfo();
    }
}
