using System;

namespace SplitBills.Models
{
    public class Settlement
    {
        public Guid Id { get; set; }

        public double Value { get; set; }

        public string TransactionId { get; set; }

        public DateTime SettledDate { get; set; }

        public string Description { get; set; }
    }
}