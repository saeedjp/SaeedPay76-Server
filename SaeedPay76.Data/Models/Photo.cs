using System;

namespace SaeedPay76.Data.Models
{
    public class Photo : BaseEntity<string>
    {
        public Photo()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

        public string Url { get; set; }
        public string DesCription { get; set; }
        public string Alt { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public UserEntity User { get; set; }
        public string  UserId { get; set; }
    }
}