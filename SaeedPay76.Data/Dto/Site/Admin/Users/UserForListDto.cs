using SaeedPay76.Data.Dto.Site.Admin.Bank;
using SaeedPay76.Data.Dto.Site.Admin.Phpto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Dto.Site.Admin.Users
{
    public class UserForListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Addres { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<PhotoForUserDetailDto> Photos { get; set; }
        public ICollection<BankCardsForDetailDto> BankCards { get; set; }
    }
}   
