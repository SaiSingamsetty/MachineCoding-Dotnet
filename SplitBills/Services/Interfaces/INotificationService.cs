using SplitBills.Models;

namespace SplitBills.Services.Interfaces
{
    public interface INotificationService
    {
        void NotifyUser(User user, Bill bill);
    }
}
