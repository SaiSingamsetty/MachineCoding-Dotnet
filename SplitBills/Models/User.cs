using System;
using System.Linq;
using System.Threading.Tasks;

namespace SplitBills.Models
{
    public class User
    {
        public Guid UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public User(string name, string email, string phoneNumber)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
