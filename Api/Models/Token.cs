using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Token : BaseEntity
    {
        [Key]
        public Guid TokenId { get; set; }

        [Required]
        public DateTime Exp { get; set; }

        [Required]
        public int UseNumber { get; set; } = 0;

        [Required]
        public bool Revoked { get; set; }
    }
}
