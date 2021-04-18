using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;

namespace SplitBills.Services.Interfaces
{
    public interface IUserService
    {
        User RegisterUser(string emailId, string phoneNum, string name);

        void AddSettlementForABill(Guid billId, string emailId, Settlement settlement);
    }
}
