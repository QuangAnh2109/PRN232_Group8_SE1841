using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class ActiveDTO<Tkey> where Tkey : struct
    {
        [Required]
        public Tkey Id { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
