using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmcs.Models
{
    public class ClaimFile
    {
        [Key]
        public int FileId { get; set; }

        public int ClaimId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        //  Navigation property
        public Claim Claim { get; set; }
    }
}