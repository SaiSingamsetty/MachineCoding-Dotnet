using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitBills.Exceptions;
using SplitBills.Models;
using SplitBills.Repository;

namespace SplitBills.Services
{
    public class UserService
    {
        public User RegisterUser(string emailId, string phoneNum, string name)
        {
            var user = new User(name, emailId, phoneNum);
            if (UserRepository.UserData.ContainsKey(user.UserId))
            {
                throw new SplitBillException(409, "USER_WITH_SAME_ID_ALREADY_EXISTS");
            }
            
            UserRepository.UserData.Add(user.UserId, user);
            return user;
        }

        public void AddSettlementForABill(Guid billId, string emailId, Settlement settlement)
        {
            var bill = BillsRepository.BillsData[billId];
            var billSplitGroup = bill.GetSplitGroup();
            if (bill.GetBillStatus() == Status.Settled)
            {
                throw new SplitBillException(400, "BILL_SETTLED_ALREADY");
            }

            var userData = UserRepository.UserData.FirstOrDefault(x =>
                x.Value.Email.Equals(emailId, StringComparison.InvariantCultureIgnoreCase)).Value;
            var userShare = billSplitGroup.GetUserSharesInTheGroup().FirstOrDefault(x => x.UserId == userData.UserId);


        }
    }
}
