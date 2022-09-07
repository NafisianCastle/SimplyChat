using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplyChatAPI.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Username { get; set; }

        [Required]
        public string Text { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SendAt { get; set; }

        [ForeignKey("Sender")]
        public int UserId { get; set; }
        public virtual User Sender { get; set; }
    }
}
