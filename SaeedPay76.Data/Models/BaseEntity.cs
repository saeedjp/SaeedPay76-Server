using System;
using System.ComponentModel.DataAnnotations;

namespace SaeedPay76.Data.Models
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
