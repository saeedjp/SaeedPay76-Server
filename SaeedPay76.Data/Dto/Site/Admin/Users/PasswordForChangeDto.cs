using System.ComponentModel.DataAnnotations;

namespace SaeedPay76.Data.Dto.Site.Admin.Users
{
    public class PasswordForChangeDto
    {
        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "پسورد باید بین 4 تا 10 رقم باشد")]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
}
