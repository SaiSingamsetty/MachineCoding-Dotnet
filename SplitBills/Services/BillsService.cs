using SplitBills.Exceptions;
using SplitBills.Models;
using SplitBills.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using SplitBills.Services.Interfaces;

namespace SplitBills.Services
{
    public class BillsService : IBillsService
    {
        private readonly INotificationService _notificationService;

        public BillsService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Bill CreateBill(string title, string desc, double billAmount, Guid userId, List<UserShare> userShares)
        {
            if (Math.Abs(userShares.Sum(x => x.GetUserShareValue()) - billAmount) != 0 )
            {
                throw new SplitBillException(400, "BILL_NOT_MATCHES_WITH_USER_SHARES");
            }

            var billSplitGroup = new SplitGroup(userShares);
            var bill = new Bill(userId, title, desc, DateTime.Now, billAmount, billSplitGroup);

            if (BillsRepository.BillsData.ContainsKey(bill.Id))
            {
                throw new SplitBillException(400, "BILL_ALREADY_EXISTS_WITH_SAME_ID");
            }

            BillsRepository.BillsData.Add(bill.Id, bill);

            foreach (var userShare in userShares)
            {
                _notificationService.NotifyUser(UserRepository.UserData[userShare.UserId], bill);
            }

            return bill;
        }

        public void SetBillStatus(Guid billId, Status status)
        {
            if (!BillsRepository.BillsData.ContainsKey(billId))
            {
                throw new SplitBillException(400, "BILL_NOT_EXISTS");
            }

            BillsRepository.BillsData[billId].SetBillStatus(status);
        }
    }
}
