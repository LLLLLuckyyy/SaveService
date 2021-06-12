using System;
using System.ComponentModel.DataAnnotations;

namespace SaveService.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[] File { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public int? UserId { get; set; }
    }
}
