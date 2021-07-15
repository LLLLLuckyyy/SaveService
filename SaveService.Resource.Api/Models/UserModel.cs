using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveService.Resources.Api.Models
{
    public partial class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
        [ForeignKey("UserId")]
        public List<FileModel> Files { get; set; } = new List<FileModel>();
    }
}
