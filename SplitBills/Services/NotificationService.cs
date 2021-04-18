using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;
using SplitBills.Services.Interfaces;

namespace SplitBills.Services
{
    public class NotificationService : INotificationService
    {
        public void NotifyUser(User user, Bill bill)
        {
            Console.WriteLine("Notified to " + user.Name);
        }
    }
}
