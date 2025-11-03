using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Token : BaseEntity<Guid>
    {
        [Required]
        public DateTime Exp { get; set; }

        [Required]
        public int UseNumber { get; set; } = 0;
    }
}
