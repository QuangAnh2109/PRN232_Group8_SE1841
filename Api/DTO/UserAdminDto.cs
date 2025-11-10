namespace Api.DTO;

public class UserAdminDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public string CenterName { get; set; } = null!;
    public string RoleName { get; set; } = null!;
    public string Status { get; set; } = null!;
}