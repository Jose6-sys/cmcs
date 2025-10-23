using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmcs.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        public int UserId { get; set; }

        [Required]
        public double HoursWorked { get; set; }

        [Required]
        public double HourlyRate { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; } = "Pending";

        //  Relationships
        public User User { get; set; }

        //  Add this collection for uploaded files
        public ICollection<ClaimFile> ClaimFiles { get; set; }
    }
}