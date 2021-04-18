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

            //Combine duplicate user shares
            var userShareMap = new Dictionary<Guid, UserShare>();
            foreach (var userShare in userShares)
            {
                if (userShareMap.ContainsKey(userShare.UserId))
                {
                    userShareMap[userShare.UserId].SetUserShareValue(userShareMap[userShare.UserId].GetUserShareValue() + userShare.GetUserShareValue());
                }
                else
                {
                    userShareMap[userShare.UserId] = userShare;
                }
            }

            _userShares = userShareMap;
        }
    }
}