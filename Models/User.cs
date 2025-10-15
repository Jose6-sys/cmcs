using System.ComponentModel.DataAnnotations;

namespace cmcs.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Lecturer, Coordinator, Manager
    }
}