using System;
using System.Collections.Generic;

namespace SaeedPay76.Data.Models
{
    public class UserEntity : BaseEntity<String>
    {
        public UserEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
        public string Name { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastActive { get; set; }
        public string City { get; set; }

        public string PhoneNumber { get; set; }
        public string Addres { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<BankCard> BankCards { get; set; }
    }
}
