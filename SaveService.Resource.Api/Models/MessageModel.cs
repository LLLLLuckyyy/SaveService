using System;
using System.ComponentModel.DataAnnotations;

namespace SaveService.Resources.Api.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public int? UserId { get; set; }
    }
}
