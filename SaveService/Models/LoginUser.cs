using System.ComponentModel.DataAnnotations;

namespace SaveService.Models
{
    public class LoginUser
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
