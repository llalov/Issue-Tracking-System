using SIT.Models.Interfaces;

namespace SIT.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment : IDentificatable
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int IssueId { get; set; }

        public virtual Issue Issue { get; set; }
    }
}