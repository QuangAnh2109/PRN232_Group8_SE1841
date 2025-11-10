namespace Api.DTO;

public class CenterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ManagerName { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class CenterDetailDto
{
    public CenterDto Center { get; set; } = null!;
    public List<UserAdminDto> Users { get; set; } = null!;
    public List<ClassDto> Classes { get; set; } = null!;
}