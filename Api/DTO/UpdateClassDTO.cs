using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class UpdateClassDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public int CenterId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

