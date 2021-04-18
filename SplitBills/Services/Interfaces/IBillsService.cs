using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Models;

namespace SplitBills.Services.Interfaces
{
    public interface IBillsService
    {
        Bill CreateBill(string title, string desc, double billAmount, Guid userId, List<UserShare> userShares);

        void SetBillStatus(Guid billId, Status status);
    }
}
