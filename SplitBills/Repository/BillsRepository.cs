using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;

namespace SplitBills.Repository
{
    public class BillsRepository
    {
        public static Dictionary<Guid, Bill> BillsData = new Dictionary<Guid, Bill>();
    }
}
