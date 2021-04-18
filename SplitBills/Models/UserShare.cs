using System;
using System.Collections.Generic;

namespace SplitBills.Models
{
    public class UserShare
    {
        public Guid UserId;

        private readonly double _value;

        private readonly List<Settlement> _settlements;

        private Status _shareStatus;

        public double GetUserShareValue()
        {
            return _value;
        }

        public List<Settlement> GetSettlements()
        {
            return _settlements;
        }

        public void SettleUserShare()
        {
            _shareStatus = Status.Settled;
        }

        public Status GetUserShareStatus()
        {
            return _shareStatus;
        }

        public UserShare(Guid userId, double value)
        {
            UserId = userId;
            _value = value;
            _settlements = new List<Settlement>();
            _shareStatus = Status.NotSettled;
        }
    }
}