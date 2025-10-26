using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Class : BaseEntity
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Api.Models.Center))]
        public int CenterId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Center Center { get; set; } = null!;

        public ICollection<Timesheet>? Timesheets { get; set; }
    }
}
