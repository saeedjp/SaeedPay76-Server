using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Dto.Site.Admin.Bank
{
    public class BankCardsForDetailDto
    {
        public string BankName { get; set; }
        public string OwnerName { get; set; }
        public string Shaba { get; set; }
        public string CardNumber { get; set; }
        public string ExpireDateMonth { get; set; }
        public string ExpireDateYear { get; set; }
    }
}
