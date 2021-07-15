using System.ComponentModel.DataAnnotations;
using SaveService.Auth.Api.Repository;

namespace SaveService.Auth.Api.Models
{
    public class LoginUser : ICommonType
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
