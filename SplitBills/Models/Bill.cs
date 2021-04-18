using System;

namespace SplitBills.Models
{
    public class Bill
    {
        public Guid Id { get; }

        private Guid UserId { get; set; }

        private string Title { get; set; }

        private string Description { get; set; }

        private DateTime BillDate { get; set; }

        private Status Status { get; set; }

        private double Amount { get; set; }

        private SplitGroup SplitGroup { get; set; }

        public void SetBillStatus(Status status)
        {
            Status = status;
        }

        public Status GetBillStatus()
        {
            return Status;
        }

        public SplitGroup GetSplitGroup()
        {
            return SplitGroup;
        }

        public Bill(Guid userId, string title, string description, DateTime billDate, double amount, SplitGroup splitGroup)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Description = description;
            BillDate = billDate;
            Amount = amount;
            SplitGroup = splitGroup;
            Status = Status.NotSettled;
        }
    }

    public enum Status
    {
        NotSettled,
        Settled
    }
}