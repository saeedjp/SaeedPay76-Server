using System;

namespace SaeedPay76.Data.Models
{
    public class BanckCard :BaseEntity<string>
    {
        public BanckCard()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = System.DateTime.Now;
        }
        public string BanckName { get; set; }
        public string Shaba { get; set; }
        public string CardNumber { get; set; }
        public string ExpireDateMonth { get; set; }
        public string ExpireDateYear { get; set; }
        public UserEntity User { get; set; }
        public string UserId { get; set; }
    }
}