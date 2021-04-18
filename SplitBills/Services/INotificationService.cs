using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;

namespace SplitBills.Services
{
    public interface INotificationService
    {
        void NotifyUser(User user, Bill bill);
    }
}
