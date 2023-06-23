using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace urs_api.Models.Implementaions
{
    public class PastProgram
    {
        [Key]
        public int programId { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        [MinLength(5), MaxLength(8)]
        public Int32 pinCode { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public string participantNames { get; set; }

        public static explicit operator PastProgram(EntityEntry<PastProgram> v)
        {
            throw new NotImplementedException();
        }
    }
}
