namespace Api.DTO;

public class UserDetailAdminDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string RoleName { get; set; } = null!;
    public string UserStatus { get; set; } = null!;
    public int? CenterId { get; set; }
    public string? CenterName { get; set; }
    public string? CenterAddress { get; set; } = null!;
    public string CenterEmail { get; set; } = null!;
    public string CenterStatus { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public List<ClassInfo>? Classes { get; set; }
}

public class ClassInfo
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = null!;
    public DateTime JoinedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = null!;
}