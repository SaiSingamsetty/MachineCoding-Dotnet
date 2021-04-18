using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBills.Models
{
    public class UserShare
    {
        public Guid UserId;

        private double _value;

        private readonly List<Settlement> _settlements;

        private Status _shareStatus;

        public double GetUserShareValue()
        {
            return _value;
        }

        public void SetUserShareValue(double val)
        {
            _value = val;
        }

        public double GetSettledValueTillNow()
        {
            return _settlements.Select(x=>x.Value).Sum();
        }

        public List<Settlement> GetSettlements()
        {
            return _settlements;
        }

        public void AddSettlementToUserShare(Settlement settlement)
        {
            settlement.Id = Guid.NewGuid();
            _settlements.Add(settlement);
            if (Math.Abs(GetSettledValueTillNow() - _value) == 0 )
            {
                SettleUserShare();
            }
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