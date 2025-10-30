using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class LoginDTO
    {
        [Required]
        public string username = null!;

        [Required]
        public string password = null!;
    }
}
