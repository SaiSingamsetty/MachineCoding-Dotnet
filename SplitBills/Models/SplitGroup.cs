using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SplitBills.Models
{
    public class SplitGroup
    {
        private readonly Guid _groupId;

        private readonly Dictionary<Guid, UserShare> _userShares;

        public Guid GetGroupId()
        {
            return _groupId;
        }

        public List<UserShare> GetUserSharesInTheGroup()
        {
            return _userShares.Select(x=>x.Value).ToList();
        }
        
        public SplitGroup(List<UserShare> userShares)
        {
            _groupId = Guid.NewGuid();
            _userShares = userShares.ToDictionary(x => x.UserId);
            //Users = userShares.Select(x => x.UserId).Distinct().ToHashSet();
        }
    }
}