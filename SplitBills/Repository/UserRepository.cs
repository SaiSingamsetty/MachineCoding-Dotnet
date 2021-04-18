using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;

namespace SplitBills.Repository
{
    public class UserRepository
    {
        public static Dictionary<Guid, User> UserData = new Dictionary<Guid, User>();
    }
}
