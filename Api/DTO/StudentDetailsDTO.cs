namespace Api.DTO
{
    public class StudentDetailsDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int? CenterId { get; set; }
        public string? CenterName { get; set; }
        public string CenterAddress { get; set; } = null!;
        public string CenterEmail { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public List<StudentClassInfo> Classes { get; set; } = new();
    }

    public class StudentClassInfo
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}

